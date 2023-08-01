using LightKeysTransfer.Abstract;
using LightKeysTransfer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightKeysTransfer.Implementation
{
    public class SecureFileContentTransferHelper : IKeyTransferHelper
    {
        public string MainText => "Securely transfer the content of some small file?";

        public KeyTransferResult Perform()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(MainText);
                Console.WriteLine();
                Console.WriteLine();
                ShowHelp();
                Console.Clear();
                var selection = ShowMenu();

                switch (selection)
                {
                    case 1:
                        ServerMode();
                        break;
                    case 2:
                        ClientMode();
                        break;
                    case 3:
                        ShowHelp();
                        selection = ShowMenu();
                        break;
                    case 4:
                        return KeyTransferResult.Incomplete;
                    default:
                        break;
                }
            }

            return KeyTransferResult.Incomplete;
        }

        private void ClientMode()
        {
            Console.WriteLine("PRESS <ENTER> to generate new public / private key pair.");
            Console.ReadLine();
            Util.GenerateRSAKeyPair();
            Console.WriteLine("New key pair has been generated.");
            Console.WriteLine("Press <ENTER> to copy the public key into clipboard.");
            Util.CopyPublicKey();
            Console.WriteLine("The public key has been copied to clipboard, press <ENTER> after pasting in the server instance to clear the clipboard.");
            Console.ReadLine();
            Util.ClearClipBoard();
            Console.WriteLine("Enter the encrypted text:");
            var response = Util.GetSensitiveText();
            var plainText = Util.DecryptText(response);
            Console.WriteLine("The text has been decrypted.");
            Console.WriteLine("Press <ENTER> to copy to clipboard");
            Console.ReadLine();
            Util.CopyToClipBoard(plainText);
            Console.WriteLine("The text has been copied.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to clear clipboard");
            Console.ReadLine();
            Util.ClearClipBoard();
            Console.WriteLine("The text has been cleared");
        }

        private void ServerMode()
        {
            Console.WriteLine("Enter the public key generated on the client instance of the application, enter the key here and immediately clear the clipboard:");
            var publicKey = Util.GetSensitiveText();
            var result = Util.InitializeRSA(publicKey);
            if (!result)
            {
                Console.WriteLine("There has been an error!");
                Console.WriteLine("Press <ENTER> to return to menu.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("The key has been loaded.");
            var fileName = GetFileName();
            if (String.IsNullOrEmpty(fileName))
            {
                return;
            }

            var content = String.Empty;

            try
            {
                content = GetFileContent(fileName);
                Console.WriteLine("Conent read, encrypting...");
                var enc = Util.EncryptText(content);
                Console.WriteLine(enc);
                Console.WriteLine();
                Console.WriteLine("Press <ENTER> to clear screen and goto previous menu.");
                Console.ReadLine();
                Console.Clear();
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine($"error occurred. {e.Message}");
                Console.WriteLine("Press <ENTER> for previous menu");
            }
        }

        private string GetFileContent(string fileName)
        {
            var sr = new StreamReader(fileName);
            var content = sr.ReadToEnd();
            sr.Close();

            return content;
        }

        private string GetFileName()
        {
            var isValidFile = false;
            var fileName = String.Empty;

            while (!isValidFile)
            {
                Console.WriteLine("Enter the path of the file whose content needs to encrypted (the file content can be upto 500 chars), or empty string for previous menu.");

                fileName = Console.ReadLine();
                if (String.IsNullOrEmpty(fileName))
                {
                    return String.Empty;
                }

                isValidFile = File.Exists(fileName);
                if (!isValidFile)
                {
                    Console.WriteLine("Invalid file path.");
                }
                if (isValidFile)
                {
                    try
                    {
                        FileInfo fi = new FileInfo(fileName);
                        if (fi.Length > 500)
                        {
                            Console.WriteLine("Error: File length longer than 500.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error occurred - {e.Message}");
                        return String.Empty;
                    }
                }
            }

            return fileName;
        }

        private int ShowMenu()
        {
            Console.WriteLine("1. Server Mode");
            Console.WriteLine("2. Client Mode");
            Console.WriteLine("3. Show Help");
            Console.WriteLine("4. Previous menu");
            Console.WriteLine();
            Console.WriteLine("Please enter your selection:");
            var response = Console.ReadLine();
            var selection = 0;
            if (Int32.TryParse(response, out selection))
            {
                return selection;
            }
            else
            {
                Console.WriteLine("Invalid selection.");
                ShowMenu();
            }

            return 0;
        }

        void ShowHelp()
        {
            Console.WriteLine("This mode allows transferring content of some small file securely without using SFTP or SCP.");
            Console.WriteLine("The file can be on the server, the content gets transferred encrypted, without getting stored on the client.");
            Console.WriteLine("Then the decrypted content can be copied into clipboard, used and clipboard can be cleared.");
            Console.WriteLine("This way, the content of file gets transferred encrypted, does not get persisted on client.");
            Console.WriteLine("Usecases; Cloud VMs and AMIs such as Jenkins etc... which write inital passwords into a file on server.");
            Console.WriteLine("Using this tool, the content would NOT be displayed in plain-text.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("The tool can run in server mode and client mode.");
            Console.WriteLine("ServerMode:");
            Console.WriteLine("Accepts file to be read, random but cryptographically strong public key.");
            Console.WriteLine("Encrypts the content and displays the encrypted content.");
            Console.WriteLine("ClientMode:");
            Console.WriteLine("Generates a cryptographically strong keypair and displays public key");
            Console.WriteLine("The public key needs to be entered in the server app:");
            Console.WriteLine("The server displays encrypted text that needs to be copied and pasted.");
            Console.WriteLine("The decrypted value can be copied.");
            Console.WriteLine("Shortcomings:");
            Console.WriteLine("Over the long-term someone might brute-force the private key and the content");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Alternatives: SCP/SFTP the file, use Mode 5 to read contents without displaying on screen and delete the file.");
            Console.WriteLine();
            Console.WriteLine("PRESS <ENTER> TO CONTINUE...");
            Console.ReadLine();
        }
    }
}
