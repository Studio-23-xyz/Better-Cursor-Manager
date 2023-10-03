# Custom Cursor System for Unity

This custom cursor system for Unity allows you to easily implement and manage custom cursors with configurable textures, hotspots, and pixel sizes. This README provides instructions on how to use this library in your Unity projects.

## Table of Contents

1. [Installation](#installation)
2. [Usage](#usage)
   - [Creating Cursor Data](#Creating CursorData)
   - [Creating a CursorCanvas prefab](#Installing CursorCanvas)
   - [Setting Up CursorManager Script](#Using CursorManager script)
3. [Example](#example)
4. [License](#license)

## Installation

### Install via Git URL
You can also use the "Install from Git URL" option from Unity Package Manager to install the package.
```
https://github.com/Studio-23-xyz/Better-Cursor-Manager.git#upm
```
## Usage

### Creating CursorData

1. In the Unity Editor menu window, Studio-23 -> Better Cursor -> Create CursorData.

2. A editor window will popup to create new cursor data. Place any sprite in "Cursor Texture" field.

3. In the "Hotspot" field, put the rect transfrom's [where cursor texture will be placed in ui] preferable pivot position [default value should be .3f , .8f]

4. In the "Pixel Size" field, put the preferable cursor's width and height [default value should be 64 , 64]

3. A "CursorCanvas" gameobject will be generated in scene hierarchy. You only need to do this procedure once for whole project as this gameobject will be persisted when scene changes.

### Installing CursorCanvas

1. In the Unity Editor menu window, Studio-23 -> Better Cursor -> Install Cursor .

2. A "CursorCanvas" prefab will be generated in scene hierarchy. You only need to do this procedure once as this prefab will be persisted when scene changes. This prefab will have canvas component with a sorting order 200 so that it will stay on top of every canvas.

### Using CursorManager script

1. The CursorCanvas gameobject will contain a CursorManager script. This script has instances. Use that instances to call function from this script.

2. Cursor manager has a default cursor field in which already predefined cursordata is assigned. You can assign your created custom cursordata in this field.


```csharp
// Example code to change the cursor when a button is clicked:
public void OnButtonClick()
{
    CursorManager.Instance.ToggleCursor(true);
}