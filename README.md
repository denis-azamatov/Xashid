# Xashid
Hashids generation tool.
## Instalation
To pack and install use following commands:
```
dotnet pack
dotnet tool install --global --add-source ./nupkg xashid
```

## Usage
Tool works in two ways.

#### Application:
```
xashid
```
![image](https://user-images.githubusercontent.com/48183905/202826710-22cc56dc-12f4-4787-a35c-ac95c7bd67a3.png)

#### Fast encoding and decoding:
```
xashid -e [value]
xashid -d [value]
```
![image](https://user-images.githubusercontent.com/48183905/202826816-1b153c00-f613-4a70-85a8-bc8b63e0ba7e.png)
