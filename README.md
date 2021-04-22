# Grouping Tool

## Description
The Nementic Grouping Tool is a small Unity tool which groups the selected GameObjects under a new parent GameObject. The parent will be created under the next common parent GameObject of the selected objects. When calling the tool a small pop-up window is shown where the name of the new parent GameObject can be set. The position of the new group can be selected in the position dropdown. You can choose between a local origin position and centered between the child GameObjects. The tool can be called via a shortcut which is configurable in Unity's shortcut manager and the right-click context menu. The pop-up window can be disabled in the preferences and a default label for the group GameObject can be set. Disabling the pop-up window in the preferences will instantly group the selected GameObjects in a group with the default name 'New Group' and the last used group position when the shortcut or the context menu entry is used. Fully supports undo and deeply nested hierarchys.

![Preview: Context menu entry on right click](Documentation~/ContextMenu.jpg)
![Preview: Preferences](Documentation~/Preferences.jpg)
![Preview: Pop-up on group creation triggered](Documentation~/PopUp.jpg)


## Setup
This tool has no dependencies other than the Unity editor itself and becomes available as soon as it is installed. The currently verified Unity version is 2020.2.

## Usage
Select multiple GameObjects in the SceneView, right-click and select the context menu entry 'Group' or use the shortcut defined in Unity's shortcut manager (default is 'Control + G').
A pop-up will open in which you can define the name and position of the new group. Click on the 'Group' button or hit enter to execute the grouping. The grouping will be cancelled if you click outside of the pop-up window.

## Features
- Group position can be choosen between centered and origin
- Customisable shortcut
- Naming pop-up for created groups
- Automatic labeling of groups
- Preferences to disable labels and naming pop-up
- Works without hierarchy or scene view open
- Complete undo
- Supports GameObjects on different hierarchy levels
- Supports GameObjects that are nested within each other
- Supports deep hierarchies - tested in a 25 layer deep hierarchy
- Supports multiple open scenes

### Support

If you'd like to support this and similar projects, buy us a coffee:</br>
<a href="https://www.buymeacoffee.com/nementic" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a>

All income will directly benefit the team in the form of coffee, tea and office plants. It might not be much, but it keeps up morale and helps us working on additional side projects.
