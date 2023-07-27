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
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(MainText);
            Console.WriteLine();
            Console.WriteLine();
            ShowHelp();
            Console.WriteLine();
            Console.WriteLine();
            return KeyTransferResult.Incomplete;
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
        }
    }
}
