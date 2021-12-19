# Unity Local Database

## What is this?
This is Source code of custom Unity local database

## How add Database To your project
- Download and Import Package to your Project
- Download and copy Source Code to Your Project

## How Does it Work?
- Add Datatbase Package to your Script
```
using LocalDataBase;
```
- Save your Data With one of Following Methods as pair of (key, Value)
```
DataBase.AddString(key, value);
DataBase.AddInt(key, value);
DataBase.AddFloat(key, value);
DataBase.AddBool(key, value);
DataBase.AddLong(key, value);
```
- Flush Data to Disk with Flush Method
```
DataBase.Flush();
```
- Get Access to Saved Record with key and one of Following Method
```
DataBase.GetString(key)
DataBase.GetInt(key)
DataBase.GetFloat(key)
DataBase.GetBool(key)
DataBase.GetLong(key)
```
- Delete Record with its key with Following Method
```
DataBase.DeleteField(key);
```

## Acknowledgements
Feel free to use and Chnage this Code based on your needs.
