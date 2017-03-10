# OSInfo
The OSInfo namespace includes many classes that return information about the current Windows installation. This includes the following:
- Architecture (String and Int representation)
- Edition (String representation)
- Name (String, ExpandedString and Enum representation) Also contains methods that will return the current and pending Computer Name.
- Product Key (String representation)
- Service Pack (String and Int representation)
- User Info (Contains Registered Organization, Registered Owner, Logged In Username, and Current Domain Name)
- Version (String and Int representation) Includes Main, Major, Minor, Build,  Revision and Number (number is Major * 10 + Minor).

# HWInfo
The HWInfo namespace includes many classes that return information about the current computer hardware. This includes the following:
- BIOS (Release Date, Version and Vendor Name)
- Network (Internal IP Address, External IP Address and Connection Status)
- OEM (Vendor Name and Product Name)
- Processor (Name and Number Of Cores)
- RAM (Total Installed Ram Size)
- Storage (System Drive Total Size, System Drive Free Space)

# ComputerInfo
This class is an instantiated class that contains all the info in the OSInfo and HWInfo classes. It extends the HWInfo.Storage class to contain all reconized drives not just the system drive.

# SecurityTools
The SecurityTools class contains methods surrounding hashing and encryption. This includes the following:
- Secure Random Number Generator
- getFileHash(HashType type, String fileName) - Generates a file hash of the supplied filename via the selected hash type (MD5, SHA1, SHA256, SHA384 and SHA512)
- CreateSalt(int size) - Creates a RNG salt for use in password hashing using the supplied length.
- CreateHash(String value, String salt) - Creates a SHA512 password hash with the supplied password and salt.
- CheckHashesMatch(String enteredPassword, String databasePassword, String databaseSalt) - Checks if the supplied password matches the supplied database password and salt. This can be used to verifiy passwords for a login system.
- GenerateRSAKeyPair() - Generates a RSA key pair for use in encryption.
- Encrypt(String publicKey, String unencryptedText) - Encrypts a string using a RSA public key.
- Decrypt(String privateKey, String encryptedText) - Decrypts a string using a RSA private key.

# CommandInfo
This class allows you to run any console command and will return the result to a string to use within your program. You can also run the command elevated and it will open in a new cmd window and show the results. Note: If elevated, result cannot be returned as a string.

# DBTools
This class contains methods to communicate with a database. It currently supports only SQLite and does not require you to import any of the SQLite libraries.
- Databse Object (Path, Password and Connection String)
- LoadDBTable(String table, Database db, DataSet ds) - Loads the specified database into specified dataset.
- SetSettings(String name, String value, Database db, DataSet ds) - Sets a seting value in a table called "Settings". Table is created automaticly if it doesn't exist.
- GetSettings(String name, Database db, DataSet ds) - Gets a setting value from a table called "Settings". Table is created automaticly if it doesn't exist.
- UpdateDBTable(String table, Database db, DataSet ds) - Commits data to database from specified dataset.

# ProgramInfo
This class returns info about the current assembly. This includes Title, Version, Description, Product Name, Copyright, Company, Startup Path and ExecutablePath.
This class also includes the folowing method to save embedded resouces to disk.
SaveResourceToDisk(String resourceName, String fileToExtractTo)

# WebTools
This class contains utilities for HTML and JSON.
- GetHtml(String url) - Returns a string of the HTML of a webpage.
- GetJson(String url, String CurrentToken) - Returns a string result of a JSON query with the provided login token.

# DateTimeHelper
This class contains two representations of the current date and time.
- CurrentShortTimeStamp - MM/dd/yy HH:mm:ss tt (01/01/17 12:00:00 PM)
- CurrentFullTimeStamp - ddd MMMM dd, yyyy hh:mm:ss tt (Sunday January 1, 2017 12:00:00 PM)

# ObjectConverters
### BreakDictionaryToString(Dictionary<String, String> dictionary)
Breaks a dictionary down into a delimited string. It seperates the keys with an "=" and the key pairs with a "&".

### ObjectToByteString(object obj)
Breaks a object down into a delimited string. It seperates each byte with a "&".

### ByteStringToObject(String bytes)
Converts a byte string to an object. You then must cast the new object as the original object type that was used in the previous method.

# InternalForms
This class contans predesigned reusable forms. 
### FixInternet
This form when run will execute some commands to attempt to fix any issues with the current internet connection. When the commands are completed the form will close automaticly. The Commands are as follows:
- netsh winsock reset catalog
- netsh interface ip reset all
- netsh interface ip delete arpcache
- nbtstat -R
- nbtstat -RR
- ipconfig /flushdns
- ipconfig /registerdns
- ipconfig /release
- ipconfig /renew
