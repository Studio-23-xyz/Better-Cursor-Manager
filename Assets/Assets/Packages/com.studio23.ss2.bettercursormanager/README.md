# Custom Cursor System for Unity

This custom cursor system for Unity allows you to easily implement and manage custom cursors with configurable textures, hotspots, and pixel sizes. This README provides instructions on how to use this library in your Unity projects.

## Table of Contents

1. [Installation](#installation)
2. [Usage](#usage)
   - [Creating CursorSettings](#creating-cursorsettings)
   - [Creating a Cursor Manager](#creating-a-cursor-manager)
   - [Setting Up UI Elements](#setting-up-ui-elements)
3. [Example](#example)
4. [License](#license)

## Installation

1. Clone or download the repository.

2. In your Unity project, navigate to `Assets > Import Package > Custom Package...`.

3. Select the downloaded package file (`CustomCursorSystem.unitypackage`) and click **Import**.

## Usage

### Creating CursorSettings

1. In the Unity Editor, right-click in your project's Assets folder.

2. Select `Create > CustomCursor > CursorSettings` to create a new CursorSettings asset.

3. Configure the CursorSettings asset with the desired cursor texture, hotspot, and pixel size.

### Creating a Cursor Manager

1. Create an empty GameObject in your scene to manage cursors.

2. Attach the `CursorManager` script to the GameObject.

3. In the Inspector, assign the `defaultCursor` field with the default CursorSettings asset you created earlier. This cursor will be used when no custom cursor is set.

### Setting Up UI Elements

1. Create UI elements, buttons, or triggers in your scene.

2. Create scripts or use Unity's UI events to trigger cursor changes.

3. Call the `SetCursor` method of the `CursorManager` with the desired `CursorSettings` asset to change the cursor when a UI element is interacted with.

```csharp
// Example code to change the cursor when a button is clicked:
public void OnButtonClick()
{
    CursorManager.Instance.SetCursor(customCursorSettings);
}
