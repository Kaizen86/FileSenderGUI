# FileSenderGUI
Graphical utility for sending files to another computer

I wasn't happy with my previous FileSender, so I remade it for WinForms. There are 3 tabs, one for sending, one for receiving and one for settings. It is very simple; select the file or destination folder (depends on which tab you are in) and press go. Below is a more indepth explanation of how to use it.

## Sending
![Image of the send tab](https://raw.githubusercontent.com/floathandthing/FileSenderGUI/master/Send%20tab.png "Image of the send tab")

Once you have setup the recipent address and port in the settings tab, you can select a file to send to them. Click the select button and locate the file. Once you do so, you can then click the send button to send a file transfer request to the recipient. If they agree to the file being sent, the file transfer will then begin. If they reject the file, you will be disconnected. At the bottom, there is a progress bar to indicate how the file transfer is doing.

## Receiving
![Image of the receive tab](https://raw.githubusercontent.com/floathandthing/FileSenderGUI/master/Receive%20tab.png "Image of the receive tab")

Before you can start accepting files, you need to specify the folder that the file will be saved into. Once you do this, the option to accept files will become available. It uses the port specified in settings, so you will need to tell the sender this number. If the sender is not connected to the same network as you, you will need to forward the port in your router settings. That is outside the scope of this guide though. Once the sender does connect, you should get a dialog box asking if you accept the file they are sending. It will contain the filename and its size. If you accept, the transfer will begin and the file will download.

## Settings
![Image of the settings tab](https://raw.githubusercontent.com/floathandthing/FileSenderGUI/master/Settings%20tab.png "Image of the settings tab")

Here you can configure the server address and port for sending files and the port that should be used for receiving files. The only thing I should mention here is that if another application is using a port, you cannot use it.
