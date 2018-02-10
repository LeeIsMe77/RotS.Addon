Only 2 lines of script MUST be included to set up the module.
Create an instance of the parser of your choosing.  These will be modulized so multiple can be loaded in a single go, and individually enabled and disabled.
Initialize the parser with the JMC object present in these script files, and set a configuration file location. This will allow your settings to persist and be changable.  This is required.

// Code //

var CONFIGURATION_LOCATION = "C:\\Temp\\Pebbleslide.xml";
var pebbleslideParser = new ActiveXObject("RotS.LineParser.PebbleslideParser");
pebbleslideParser.Initialize(jmc, CONFIGURATION_LOCATION); 

// End Code //

At some point you will want to know your character name.  That is currently a property.

// Code //

pebbleslideParser.CharacterName = "Nina";

// End Code //

If the configuration files have not been altered, the default setting for a module is disabled.  To enable a module, call Enable and to turn off call Disable:

// Code //

pebbleslideParser.Enable();
pebbleslideParser.Disable();

// End Code //

 And on JMC closing, your settings will be saved.  Consider, then, you could make custom aliases to enable and disable modules.

// Code (Alias) //

#alias {pebbleslideOn} { #script { pebbleslideParser.Enable(); }; }
#alias {pebbleslideOff} { #script { pebbleslideParser.Disable();}; }

// End Code //




NOT IMPLEMENTED

Database Configuration:
Exception logging will be done within a database of your choosing;  Otherwise, output to a window of your choosing.  Some modules will rely on database connections.

// Code //

var DATABASE_TYPE_NONE = 0;
var DATABASE_TYPE_SQL = 1;
var DATABASE_TYPE_OLEDB = 2;
var DATABASE_TYPE_ODBC = 3;
var SQL_CONNECTION_STRING = "Data Source=localhost;Initial Catalog=RotS;Integrated Security=SSPI;"
pebbleslideParser.ConfigureDatabaseConnection(SQL_CONNECTION_STRING, DATABASE_TYPE_SQL);

// End Code //
