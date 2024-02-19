# Galaxy Message Flow Studio
A MSBF editor for Super Mario Galaxy 2

![image](https://github.com/SuperHackio/GalaxyMessageFlowStudio/assets/44330283/0c874f5b-b8a4-42dc-987c-eedb3cdb78e9)

You will need the .NET 8.0 runtime to be able to use this program.

> *Note: This program does not support High DPI modes.*

## How to use
Using this program is easy.<br/>Once you have it open, go to File > Open, and select your Message Archive. You will then be prompted to input the file name. By default, the Archive name is used, and if you are just editing NPC text inside of a zone, then you can just click OK and it will load the MSBT/MSBF. (If you do this to a file that does not have an MSBF, a new one will be created for you)

When saving your file, you will be given the same filename prompt. Usually you can just press OK on that menu without changing anything.

> *SAVE WARNING: When saving, the archive that is loaded is the current version of the archive that is on your disk. However, the MSBT labels that are used for generating the MSBF are the ones from when you opened the file. If you edited your MSBT between opening the MSBF and saving, your Message Label IDs may get messed up. In the event that this occurs, simply File > Open, choose your archive, and fix any Message Nodes that do not look right to you.*

On the left, there are 2 panels: The Node toolbox (top), and the Properties menu (Bottom)<br/>
On the right, there is the Editor View

### Node Types
- Condition Nodes (Green): These let you perform conditional checks (such as showing two buttons to the player to let them make a selection)
- End of Flow Nodes (Red): These indicate that the flow path has ended and should restart. Generally you put these at the end of all flow paths.
- Entry Nodes (Yellow): These are the Starting Points for your flow nodes. Put the NPC Message Label in here to link it to a Message Request from the game.
- Event Nodes (Blue): These nodes will make things happen. The functionalities change depending on who calls these nodes.
- Message Nodes (Grey): These nodes reference MSBT labels, and thus decide what text is displayed.

For more information on Nodes, refer to the [Luma's Workshop Wiki](https://www.lumasworkshop.com/wiki/MSBF_(File_Format))'s MSBF documentation.

### The Editor
The editor is very simple to use.

- Add nodes by dragging them from the Toolbox into the Editor View
- Delete nodes by Right clicking the Coloured strip at the top of the node, and then choose "Delete Node"
- Connect nodes by clicking and dragging the Connector points. Connect any INPUT to any OUTPUT.
  - "Circle" connector points are always OUTPUT
  - "Square" connector points are always INPUT
- Disconnect nodes by Right clicking the connection after highlighting it (when it is highlighted, it will turn Blue)

## Compiling
If you'd like to compile this yourself, you will need the [Hack.io Libraries](https://github.com/SuperHackio/Hack.io)<br/>
Ensure the following file structure:
- GalaxyMessageFlowStudio
  - GalaxyMessageFlowStudio.sln
- Hack.io
  - Hack.io
  - Hack.io.MSBT
  - Hack.io.MSBF
You will also need .Net 8.0

## Credits:
- Super Hackio (Main programmer, Hack.io library author)
- @jupahe64 (UI assistance)
- [DebugST](https://github.com/DebugST/) (Node UI library author)
