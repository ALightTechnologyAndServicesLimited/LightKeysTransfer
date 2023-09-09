using LightKeysTransfer.Abstract;
using LightKeysTransfer.Common;
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
        readonly CryptHelper cryptHelper = new();

        public string MainText => "Securely transfer the content of some small file?";
        public KeyTransferResult Perform()
        {
            var taskId = ShowSubTasksMenu();

            return taskId switch
            {
                1 => ClientClipBoardToServer(),
                2 => ServerToClient(true),
                3 => ServerToClient(false),
                4 => KeyTransferResult.Incomplete,
                _ => KeyTransferResult.Incomplete,
            };
        }

        private KeyTransferResult ServerToClient(bool smallFile)
        {
            while (true)
            {
                int selection = ShowFileTransferMenu();

                switch (selection)
                {
                    case 1:
                        ServerModeFiles(smallFile);
                        break;
                    case 2:
                        ClientModeFiles(smallFile);
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
        }

        private int ShowFileTransferMenu()
        {
            Console.Clear();
            //Console.WriteLine(MainText);
            Console.WriteLine();
            Console.WriteLine();
            ShowHelp();
            Console.Clear();
            var selection = ShowMenu();
            return selection;
        }

        private KeyTransferResult ClientClipBoardToServer()
        {
            Console.Clear();
            Console.WriteLine("Enter the text below and press <ENTER>");
            var secret = CryptHelper.GetSensitiveText(false);
            return HandleSecretData(secret);
        }

        private KeyTransferResult HandleSecretData(string secret)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Write to a file?");
                Console.WriteLine("2. Replace some text in a file?");
                Console.WriteLine("3. End");
                Console.WriteLine("Please make valid choice:");

                var response = Console.ReadLine();
                if (Int32.TryParse(response, out int responseInt))
                {
                    if (responseInt < 1 || responseInt > 3) continue;

                    switch (responseInt)
                    {
                        case 1:
                            return HandleWriteToFile(secret);
                        case 2:
                            return HandleReplaceText(secret);
                        case 3:
                            return KeyTransferResult.Incomplete;
                    }
                }

                Console.WriteLine("Invalid response, press <ENTER>");
                Console.ReadLine();
            }
        }

        private KeyTransferResult HandleReplaceText(string secret)
        {
            try
            {
                Console.WriteLine("Enter path to text file.");
                var filePath = Console.ReadLine();
                Console.WriteLine("Replace text pattern can be something like:");
                Console.WriteLine("Approach-1:");
                Console.WriteLine("<abcd>[SECRET]</abcd> - in this case [SECRET] would be replaced.");
                Console.WriteLine("Use [SECRET] as the placeholder, no wild-cards");
                Console.WriteLine("Example \"key1\": \"[SECRET]\",");
                Console.WriteLine();
                Console.WriteLine("or");
                Console.WriteLine("Approach-2:");
                Console.WriteLine("XXXXXXX - in this case XXXXXXX would be replaced");
                Console.WriteLine("If you know the exact content to be replaced, do NOT use [SECRET] anywhere in the placeholder");
                Console.WriteLine("Please enter a valid pattern:");
                var response = Console.ReadLine();

                var sr = new StreamReader(filePath);
                var content = sr.ReadToEnd();
                sr.Close();

                if (response.Contains("[SECRET]", StringComparison.CurrentCulture))
                {
                    var tokens = response.Split("[SECRET]");
                    if (tokens.Length != 2)
                    {
                        Console.WriteLine("Error in pattern.");
                        return KeyTransferResult.Errored;
                    }

                    var newContent = new StringBuilder();
                    if (content.Contains(tokens[0], StringComparison.CurrentCulture))
                    {
                        if (content.IndexOf(tokens[1]) > 0)
                        {
                            newContent.Append(content.AsSpan(0, content.IndexOf(tokens[0])));
                        }

                        newContent.Append(secret);

                        if (content.IndexOf(tokens[2]) > 0)
                        {
                            newContent.Append(content.AsSpan((content.IndexOf(tokens[2]))));

                            var sw = new StreamWriter(filePath);
                            sw.Write(newContent.ToString());
                            sw.Close();

                            Console.WriteLine("Success. Press <ENTER> to continue...");
                            Console.ReadLine();
                            return KeyTransferResult.Success;
                        }
                        else
                        {
                            Console.WriteLine("Pattern Not Found Press <ENTER> to continue...");
                            Console.ReadLine();
                            return KeyTransferResult.Incomplete;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Pattern Not Found Press <ENTER> to continue...");
                        Console.ReadLine();
                        return KeyTransferResult.Incomplete;
                    }
                }
                else
                {
                    //var newContent = new StringBuilder();
                    if (content.Contains(response, StringComparison.CurrentCulture))
                    {
                        var newContent = content.Replace(response, secret);
                        var sw = new StreamWriter(filePath);
                        sw.Write(newContent.ToString());
                        sw.Close();

                        Console.WriteLine("Success. Press <ENTER> to continue...");
                        Console.ReadLine();
                        return KeyTransferResult.Success;
                    }
                    else
                    {
                        Console.WriteLine("Pattern Not Found. Press <ENTER> to continue...");
                        Console.ReadLine();
                        return KeyTransferResult.Incomplete;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Press <ENTER> to continue");
                return KeyTransferResult.Errored;
            }

            return KeyTransferResult.Incomplete;
        }

        private KeyTransferResult HandleWriteToFile(string secret)
        {
            try
            {
                Console.WriteLine("Enter path to the file:");
                var filePath = Console.ReadLine();

                var sw = new StreamWriter(filePath);
                sw.Write(secret);
                sw.Close();

                Console.WriteLine("Done! Press <ENTER> to continue...");
                Console.ReadLine();
                return KeyTransferResult.Success;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return KeyTransferResult.Errored;
            }
        }

        private int ShowSubTasksMenu()
        {
            var flag = true;

            while (flag)
            {
                Console.Clear();
                Console.WriteLine("1. Transfer small information directly by pasting from clipboard of client to server?");
                Console.WriteLine("2. Encrypt and transfer content of small file from server to client clipboard?");
                Console.WriteLine("3. Encrypt and transfer content of medium sized file from server to client clipboard?");
                Console.WriteLine("4. Previous menu");
                Console.WriteLine("Please make valid selection and press <ENTER>");
                var response = Console.ReadLine();
                if (Int32.TryParse(response, out int responseInt))
                {
                    if (responseInt >= 1 && responseInt <= 4)
                    {
                        return responseInt;
                    }
                }
                Console.WriteLine("Invalid Response, press <ENTER>");
                Console.ReadLine();
            }

            return 0;
        }

        private void ClientModeFiles(bool smallFile)
        {
            Console.WriteLine("PRESS <ENTER> to generate new public / private key pair.");
            Console.ReadLine();
            cryptHelper.GenerateRSAKeyPair();
            Console.WriteLine("New key pair has been generated.");
            Console.WriteLine("Press <ENTER> to copy the public key into clipboard.");
            Console.ReadLine();
            cryptHelper.CopyPublicKey();
            Console.WriteLine("The public key has been copied to clipboard, press <ENTER> after pasting in the server instance to clear the clipboard.");
            Console.ReadLine();
            CryptHelper.ClearClipBoard();
            if (!smallFile)
            {
                //Get Key and IV
                Console.WriteLine("Enter the encrypted Key from the server:");
                var key = CryptHelper.GetSensitiveText();
                Console.WriteLine("Enter the encrypted IV from the server:");
                var iv = CryptHelper.GetSensitiveText();

                cryptHelper.ImportTripleDES(key, iv);
            }
            Console.WriteLine("Enter the encrypted text:");
            var response = CryptHelper.GetSensitiveText();
            string plainText = String.Empty;
            if (smallFile)
            {
                plainText = cryptHelper.DecryptRSA(response);
            }
            else
            {
                plainText = cryptHelper.DecryptTripleDES(response);
            }

            Console.WriteLine("The text has been decrypted.");

            Console.WriteLine("Press <ENTER> to copy to clipboard");
            Console.ReadLine();
            CryptHelper.CopyToClipBoard(plainText);
            Console.WriteLine("The text has been copied.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to clear clipboard");
            Console.ReadLine();
            CryptHelper.ClearClipBoard();
            Console.WriteLine("The text has been cleared");
        }

        private void ServerModeFiles(bool smallFile)
        {
            Console.WriteLine("Enter the public key generated on the client instance of the application, enter the key here and immediately clear the clipboard:");
            var publicKey = CryptHelper.GetSensitiveText();
            var result = cryptHelper.InitializeRSA(publicKey);
            if (!result)
            {
                Console.WriteLine("There has been an error!");
                Console.WriteLine("Press <ENTER> to return to menu.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("The key has been loaded.");

            if (!smallFile)
            {
                // Generate Key, IV
                cryptHelper.GenerateNewTripleDES();
                cryptHelper.GetEncryptedTripleDESKey();
                Console.WriteLine("Here is the Key:");
                Console.WriteLine(cryptHelper.GetEncryptedTripleDESKey());
                Console.WriteLine("Press <Enter> to continue");
                Console.ReadLine();
                Console.WriteLine("Here is the IV:");
                Console.WriteLine(cryptHelper.GetEncryptedTripleDESIV());
                Console.WriteLine("Press <Enter> to continue");
                Console.ReadLine();
            }

            var fileName = GetFileName();
            if (String.IsNullOrEmpty(fileName))
            {
                return;
            }

            try
            {
                string? content = GetFileContent(fileName);
                Console.WriteLine("Content has been read.");
                int selection = ShowFilePartMenu();
                var contentToEncrypt = String.Empty;

                switch (selection)
                {
                    case 1:
                        contentToEncrypt = content;
                        break;
                    case 2:
                        var lines = content.Split("\n");
                        var validSelection = false;

                        while (!validSelection)
                        {
                            Console.WriteLine($"Enter the line number ({lines.Count()} lines found.):");
                            var lnn = Console.ReadLine();
                            if (Int32.TryParse(lnn, out int lnnNum))
                            {
                                if (lnnNum > 0 && lnnNum <= lines.Count())
                                {
                                    contentToEncrypt = lines[lnnNum - 1];
                                    validSelection = true;
                                }
                            }
                        }
                        break;
                    case 3:
                        return;
                    default:
                        break;
                }

                Console.WriteLine("Conent read, encrypting...");
                string enc = String.Empty;
                if (smallFile)
                {
                    enc = cryptHelper.EncryptRSA(contentToEncrypt);
                }
                else
                {
                    enc = cryptHelper.EncryptTripleDES(contentToEncrypt);
                }
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
                Console.ReadLine();
            }
        }

        private int ShowFilePartMenu()
        {
            bool hasSelectionBeenMade = false;
            while (!hasSelectionBeenMade)
            {
                Console.WriteLine("1. Encrypt entire contents?");
                Console.WriteLine("2. Particular line based on number?");
                Console.WriteLine("3. End");
                Console.WriteLine("Please make a selection: ");
                var selection = Console.ReadLine();

                if (Int32.TryParse(selection, out int selectionAsInt))
                {
                    if (selectionAsInt > 0 && selectionAsInt < 4)
                    {
                        return selectionAsInt;
                    }
                }
            }

            return 0;
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
                        FileInfo fi = new(fileName);
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
            if (Int32.TryParse(response, out int selection))
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
            Console.WriteLine("This mode allows transferring content of some small files, medium sized files securely without using SFTP or SCP.");
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
            Console.WriteLine("*** Option - 3: For medium sized files, encrypted symmetric key gets displayed, then encrypted content");
            Console.WriteLine("*** Option - 2: For small sized files, encrypted content gets displayed.");
            Console.WriteLine("ClientMode:");
            Console.WriteLine("Generates a cryptographically strong keypair and displays public key");
            Console.WriteLine("The public key needs to be entered in the server app:");
            Console.WriteLine("*** Option - 3: For medium sized files, copy and paste the encrypted key from server, then the encrypted content.");
            Console.WriteLine("*** Option - 2: For small sized files, copy and paste encrypted text from server.");
            Console.WriteLine("Then the decrypted value can be copied into clipboard.");
            Console.WriteLine("Shortcomings:");
            Console.WriteLine("Over the long-term someone might brute-force the private key and the content");
            Console.WriteLine();
            Console.WriteLine("PRESS <ENTER> TO CONTINUE...");
            Console.ReadLine();
        }
    }
}
