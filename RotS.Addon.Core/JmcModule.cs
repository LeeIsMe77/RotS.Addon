namespace RotS.Addon.Core {

	#region Directives
	using System;
	using System.Data.Common;
	using System.IO;
	using System.Runtime.InteropServices;
	using System.Text.RegularExpressions;
	using System.Xml.Linq;
	using RotS.Addon.Core.Common;
	using RotS.Addon.Core.Extensions;
	using TTCOREEXLib;
	#endregion

	/// <summary>
	/// The abstract class providing the framework for a <seealso cref="JmcModule" /> to handle events presented by a <seealso cref="JmcObj" />.
	/// </summary>
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[Guid("A82D71F8-B2A5-49D9-9A7C-1A9EB46AD86E")]
	[ProgId("RotS.Addon.JmcModule")]
	public abstract class JmcModule {

		private string _configurationFile;

		#region Properties

		#region ConnectionString

		/// <summary>
		/// Gets the connection string.
		/// </summary>
		/// <value>The connection string.</value>
		public string ConnectionString { get; private set; }

		#endregion

		#region ConnectionStringConfigured

		/// <summary>
		/// Gets a value indicating whether [connection string configured].
		/// </summary>
		/// <value><c>true</c> if [connection string configured]; otherwise, <c>false</c>.</value>
		public bool ConnectionStringConfigured => !string.IsNullOrWhiteSpace(this.ConnectionString) && this.ConnectionStringBuilderType != null;

		#endregion

		#region ConnectionStringType

		/// <summary>
		/// Gets the type of the connection string.
		/// </summary>
		/// <value>The type of the connection string.</value>
		public Type ConnectionStringBuilderType { get; private set; }

		#endregion

		#region Enabled

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="JmcModule"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		protected bool Enabled { get; private set; }
		#endregion

		#region JmcObject

		/// <summary>
		/// Gets the JMC object.
		/// </summary>
		/// <value>The JMC object.</value>
		public JmcObj JmcObject { get; private set; }

		#endregion

		#region SQLSchema

		/// <summary>
		/// Gets the SQL schema.
		/// </summary>
		/// <value>The SQL schema.</value>
		protected virtual string SQLSchema { get; }

		#endregion

		#region VerboseLoggingEnabled

		/// <summary>
		/// Gets or sets a value indicating whether [verbose logging enabled].
		/// </summary>
		/// <value><c>true</c> if [verbose logging enabled]; otherwise, <c>false</c>.</value>
		public bool VerboseLoggingEnabled { get; set; } = true;

		#endregion

		#region VerboseLoggingOutputWindow

		/// <summary>
		/// Gets or sets the verbose logging output window.
		/// </summary>
		/// <value>The verbose logging output window.</value>
		public int VerboseLoggingOutputWindow { get; set; } = 9;

		#endregion

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="JmcModule"/> class.
		/// </summary>
		protected JmcModule() { }

		#endregion

		#region JMC Event Handlers

		/// <summary>
		/// JMCs the object connected.
		/// </summary>
		private void JmcObject_Connected() {
			this.Connected();
		}

		/// <summary>
		/// JMCs the object connect lost.
		/// </summary>
		private void JmcObject_ConnectLost() {
			this.ConnectionLost();
		}

		/// <summary>
		/// JMCs the object disconnected.
		/// </summary>
		private void JmcObject_Disconnected() {
			this.Disconnected();
		}

		/// <summary>
		/// JMCs the object incoming.
		/// </summary>
		/// <param name="bstrLine">The BSTR line.</param>
		private void JmcObject_Incoming(string bstrLine) {
			this.Incoming(bstrLine);
		}

		/// <summary>
		/// JMCs the object input.
		/// </summary>
		/// <param name="strInput">The string input.</param>
		private void JmcObject_Input(string strInput) {
			this.Input(strInput);
		}

		/// <summary>
		/// JMCs the object load.
		/// </summary>
		private void JmcObject_Load() {
			this.Load();
		}

		/// <summary>
		/// JMCs the object multi incoming.
		/// </summary>
		private void JmcObject_MultiIncoming() {
			this.MultiIncoming();
		}

		/// <summary>
		/// JMCs the object pre timer.
		/// </summary>
		/// <param name="ID">The timer ID.</param>
		private void JmcObject_PreTimer(int ID) {
			this.PreTimer(ID);
		}

		/// <summary>
		/// JMCs the object prompt.
		/// </summary>
		private void JmcObject_Prompt() {
			this.Prompt();
		}

		/// <summary>
		/// JMCs the object telnet.
		/// </summary>
		private void JmcObject_Telnet() {
			this.Telnet();
		}

		/// <summary>
		/// JMCs the object timer.
		/// </summary>
		/// <param name="ID">The timer ID.</param>
		private void JmcObject_Timer(int ID) {
			this.Timer(ID);
		}

		/// <summary>
		/// JMCs the object unload.
		/// </summary>
		private void JmcObject_Unload() {
			this.Unload();
		}

		#endregion

		#region Methods

		#region Configuration

		/// <summary>
		/// Called when the configuration needs to be loaded.
		/// </summary>
		private void ConfigurationSettingsLoad() {
			try {
				XElement configuration;
				if (File.Exists(_configurationFile)) {
					configuration = XElement.Load(_configurationFile);
				}
				else {
					configuration = new XElement(this.GetType().Name);
				}
				var enabled = configuration.SafeAttributeValue(nameof(JmcModule.Enabled), this.Enabled);
				if (enabled) {
					this.Enable();
				}
				else {
					this.Disable();
				}
				this.VerboseLoggingEnabled = configuration.SafeAttributeValue(nameof(JmcModule.VerboseLoggingEnabled), this.VerboseLoggingEnabled);
				this.VerboseLoggingOutputWindow = configuration.SafeAttributeValue(nameof(JmcModule.VerboseLoggingOutputWindow), this.VerboseLoggingOutputWindow);
				this.OnConfigurationSettingsLoaded(configuration);
			}
			catch { }
		}

		/// <summary>
		/// Configurations the settings save.
		/// </summary>
		private void ConfigurationSettingsSave() {
			try {
				var configuration = new XElement(this.GetType().Name);
				configuration.Add(
					new XAttribute(nameof(JmcModule.Enabled), this.Enabled),
					new XAttribute(nameof(JmcModule.VerboseLoggingEnabled), this.VerboseLoggingEnabled),
					new XAttribute(nameof(JmcModule.VerboseLoggingOutputWindow), this.VerboseLoggingOutputWindow)
					);
				this.OnConfigurationSettingsSaved(configuration);
				configuration.Save(_configurationFile, SaveOptions.None);
			}
			catch {
				/* TODO: I need to find a way to log exceptions without a database... Maybe the EventViewer? */
			}
		}

		#endregion

		#region Event Processing

		/// <summary>
		/// Called when the connection to the server has been established.
		/// </summary>
		private void Connected() {
			try {
				if (this.Enabled && this.JmcObject.IsConnected != 0) {
					this.OnConnected();
				}
			}
			catch { }
		}

		/// <summary>
		/// Called when the connection to the server has been lost for unknown causes.
		/// </summary>
		private void ConnectionLost() {
			try {
				if (this.Enabled && this.JmcObject.IsConnected != 0) {
					this.ConnectionLost();
				}
			}
			catch { }
			finally {
				this.ConfigurationSettingsSave();
			}
		}

		/// <summary>
		/// Called when the client has terminated the session to the server.
		/// </summary>
		private void Disconnected() {
			try {
				if (this.Enabled && this.JmcObject.IsConnected != 0) {
					this.OnDisconnected();
				}
			}
			catch { }
			finally {
				this.ConfigurationSettingsSave();
			}
		}

		/// <summary>
		/// Called when an incoming line has been received from the server.
		/// </summary>
		/// <param name="incomingLine">The incoming line.</param>
		private void Incoming(string incomingLine) {
			try {
				if (this.Enabled && this.JmcObject.IsConnected != 0) {
					var cleanString = this.JmcObject.ToText(incomingLine);
					cleanString = Regex.Replace(incomingLine, @"^R? ?(?:(?:Mind|Mount|HP|MV|S):[a-zA-Z ]+ ?)*(?:, [a-zA-Z,\-' ]+:[a-zA-Z ]+)*?>", string.Empty);
					this.OnIncoming(cleanString);
				}
			}
			catch { }
		}

		/// <summary>
		/// Called when the client has sent input to the server.
		/// </summary>
		/// <param name="input">The input.</param>
		private void Input(string input) {
			try {
				if (this.Enabled && this.JmcObject.IsConnected != 0) {
					this.OnInput(input);
				}
			}
			catch { }
		}

		/// <summary>
		/// Called when an instance of the <seealso cref="JmcObj"/> client has been created..
		/// </summary>
		private void Load() {
			try {
				if (this.Enabled && this.JmcObject.IsConnected != 0) {
					this.OnLoad();
				}
			}
			catch { }
		}

		/// <summary>
		/// I have no freaking idea.
		/// </summary>
		private void MultiIncoming() {
			try {
				if (this.Enabled && this.JmcObject.IsConnected != 0) {
					this.OnMultiIncoming();
				}
			}
			catch { }
		}

		/// <summary>
		/// Called prior to a timer being firing.
		/// </summary>
		/// <param name="timerID">The timer identifier.</param>
		private void PreTimer(int timerID) {
			try {
				if (this.Enabled && this.JmcObject.IsConnected != 0) {
					this.OnPreTimer(timerID);
				}
			}
			catch { }
		}

		/// <summary>
		/// I have no freaking idea.
		/// </summary>
		private void Prompt() {
			try {
				if (this.Enabled && this.JmcObject.IsConnected != 0) {
					this.OnPrompt();
				}
			}
			catch { }
		}

		/// <summary>
		/// I have no freaking idea.
		/// </summary>
		private void Telnet() {
			try {
				if (this.Enabled && this.JmcObject.IsConnected != 0) {
					this.OnTelnet();
				}
			}
			catch { }
		}

		/// <summary>
		/// Called when a timer has fired.
		/// </summary>
		/// <param name="timerID">The timer identifier.</param>
		private void Timer(int timerID) {
			try {
				if (this.Enabled && this.JmcObject.IsConnected != 0) {
					this.OnTimer(timerID);
				}
			}
			catch { }
		}

		/// <summary>
		/// Called when the client of the <seealso cref="JmcObj"/> has been closed.
		/// </summary>		
		private void Unload() {
			try {
				if (this.Enabled && this.JmcObject.IsConnected != 0) {
					this.OnUnload();
				}
			}
			catch { }
			finally {
				this.ConfigurationSettingsSave();
			}
		}

		#endregion

		#region Public COM accessible methods

		/// <summary>
		/// Enables this instance of the <seealso cref="JmcModule"/>.
		/// </summary>
		[ComVisible(true)]
		public void Enable() {
			if (this.JmcObject == null) {
				throw new ModuleException(null, $@"The JMC object is not defined.  Ensure the initialize method has been called.");
			}
			try {
				this.Log($@"Enabling {this.GetType().FullName}", JmcColors.Green);
				this.Enabled = true;
				this.OnEnable();
			}
			catch { }
		}

		/// <summary>
		/// Disables this instance of the <seealso cref="JmcModule"/>.
		/// </summary>
		[ComVisible(true)]
		public void Disable() {
			if (this.JmcObject == null) {
				throw new ModuleException(null, $@"The JMC object is not defined.  Ensure the initialize method has been called.");
			}
			try {
				this.Log($@"Disabling {this.GetType().FullName}", JmcColors.Red);
				this.Enabled = false;
				this.OnDisable();
			}
			catch { }
		}

		/// <summary>
		/// Initializes the specified JMC object.
		/// </summary>
		/// <param name="jmcObject">The JMC object.</param>
		/// <param name="configurationFile">The configuration file.</param>
		[ComVisible(true)]
		public void Initialize(JmcObj jmcObject, string configurationFile) {
			this.JmcObject = jmcObject ?? throw new ArgumentNullException(nameof(jmcObject), @"The JMC Object cannot be null.");
			_configurationFile = configurationFile;
			try {
				this.JmcObject.Connected += this.JmcObject_Connected;
				this.JmcObject.ConnectLost += this.JmcObject_ConnectLost;
				this.JmcObject.Disconnected += this.JmcObject_Disconnected;
				this.JmcObject.Incoming += this.JmcObject_Incoming;
				this.JmcObject.Input += this.JmcObject_Input;
				this.JmcObject.Load += this.JmcObject_Load;
				this.JmcObject.MultiIncoming += this.JmcObject_MultiIncoming;
				this.JmcObject.PreTimer += this.JmcObject_PreTimer;
				this.JmcObject.Prompt += this.JmcObject_Prompt;
				this.JmcObject.Telnet += this.JmcObject_Telnet;
				this.JmcObject.Timer += this.JmcObject_Timer;
				this.JmcObject.Unload += this.JmcObject_Unload;
				this.OnInitialize();
				this.ConfigurationSettingsLoad();
			}
			catch { }
		}

		/// <summary>
		/// Configures the database connection.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="connectionBuilderStringType">Type of the connection builder string.</param>
		[ComVisible(true)]
		public void ConfigureDatabaseConnection(string connectionString, DatabaseType databaseType) {
			if (this.JmcObject == null) {
				throw new ModuleException(null, $@"The JMC object is not defined.  Ensure the initialize method has been called.");
			}
			this.ConnectionString = null;
			this.ConnectionStringBuilderType = null;

			var connectionStringBuilder = databaseType == DatabaseType.SQLServer
				? (DbConnectionStringBuilder)new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString)
				: databaseType == DatabaseType.OleDB
					? (DbConnectionStringBuilder)new System.Data.OleDb.OleDbConnectionStringBuilder(connectionString)
					: databaseType == DatabaseType.ODBC
						? (DbConnectionStringBuilder)new System.Data.Odbc.OdbcConnectionStringBuilder(connectionString)
						: null;

			if (connectionStringBuilder == null) {
				this.Log(@"The database connection has been disabled.", JmcColors.Red);
				return;
			}

			try {
				using (var connection = connectionStringBuilder.CreateConnection()) {
					connection.Open();
				}
				this.ConnectionString = connectionString;
				this.ConnectionStringBuilderType = connectionStringBuilder.GetType();
				this.Log($@"A connection has been established successfully. Connection registered.", JmcColors.Green);
			}
			catch (Exception caught) {
				this.Log($@"Failure Configuring Database Connection: {caught.Message}", JmcColors.Red);
				throw new ModuleException(caught, $@"Failure Configuring Database Connection: {caught.Message}");
			}
		}

		#endregion

		/// <summary>
		/// Logs the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="color">The color.</param>
		public void Log(string message, JmcColors color) {
			if (this.VerboseLoggingEnabled) {
				this.DisplayMessage(this.VerboseLoggingOutputWindow, message, color);
			}
		}

		/// <summary>
		/// Displays the message.
		/// </summary>
		/// <param name="window">The window.</param>
		/// <param name="message">The message.</param>
		/// <param name="color">The color.</param>
		public void DisplayMessage(int window, string message, JmcColors color) {
			this.JmcObject.wOutput(window, message, color.GetJmcColorValue());
		}

		#endregion

		#region Cascaded Methods

		#region Configuration

		/// <summary>
		/// Called when the configuration settings must be loaded.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		protected virtual void OnConfigurationSettingsLoaded(XElement configuration) { }

		/// <summary>
		/// Called when the configuration settings must be saved.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		protected virtual void OnConfigurationSettingsSaved(XElement configuration) { }

		#endregion

		#region JmcEvent Processing

		/// <summary>
		/// Called when the connection to the server has been established.
		/// </summary>
		protected virtual void OnConnected() { }

		/// <summary>
		/// Called when the connection to the server has been lost for unknown causes.
		/// </summary>
		protected virtual void OnConnectionLost() { }

		/// <summary>
		/// Called when the client has terminated the session to the server.
		/// </summary>
		protected virtual void OnDisconnected() { }

		/// <summary>
		/// Called when an incoming line has been received from the server.
		/// </summary>
		/// <param name="incomingLine">The incoming line.</param>
		protected virtual void OnIncoming(string incomingLine) { }

		/// <summary>
		/// Called when the client has sent input to the server.
		/// </summary>
		/// <param name="input">The input.</param>
		protected virtual void OnInput(string input) { }

		/// <summary>
		/// Called when an instance of the <seealso cref="JmcObj"/> client has been created..
		/// </summary>
		protected virtual void OnLoad() { }

		/// <summary>
		/// I have no freaking idea.
		/// </summary>
		protected virtual void OnMultiIncoming() { }

		/// <summary>
		/// Called prior to a timer being firing.
		/// </summary>
		/// <param name="timerID">The timer identifier.</param>
		protected virtual void OnPreTimer(int timerID) { }

		/// <summary>
		/// I have no freaking idea.
		/// </summary>
		protected virtual void OnPrompt() { }

		/// <summary>
		/// I have no freaking idea.
		/// </summary>
		protected virtual void OnTelnet() { }

		/// <summary>
		/// Called when a timer has fired.
		/// </summary>
		/// <param name="timerID">The timer identifier.</param>
		protected virtual void OnTimer(int timerID) { }

		/// <summary>
		/// Called when the client of the <seealso cref="JmcObj"/> has been closed.
		/// </summary>
		protected virtual void OnUnload() { }

		#endregion

		/// <summary>
		/// Called when this instance is disabled..
		/// </summary>
		protected virtual void OnDisable() { }

		/// <summary>
		/// Called when this instance is enabled.
		/// </summary>
		protected virtual void OnEnable() { }

		/// <summary>
		/// Called when the <seealso cref="JmcModule" /> has completed preliminary initialization.
		/// </summary>
		protected virtual void OnInitialize() { }

		#endregion

	}

}
