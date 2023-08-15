using LightKeysTransfer;
using LightKeysTransfer.Abstract;
using LightKeysTransfer.Entities;
using LightKeysTransfer.Implementation;
using Microsoft.Extensions.DependencyInjection;

PrintITStartupInfo();

try
{
    var servicesProvider = new ServiceCollection();
    servicesProvider.AddSingleton<IKeyTransferHelper, SecureFileContentTransferHelper>();

    var provider = servicesProvider.BuildServiceProvider();
    using (provider)
    {
        var helpers = provider.GetServices<IKeyTransferHelper>();

        while (true)
        {
            var selected = ShowLevel1Menu(helpers);
            if (selected == null) return;

            try
            {
                var result = selected.Perform();

                switch (result)
                {
                    case KeyTransferResult.Success:
                        Console.WriteLine("Seems like what you have tried to do has completed successfully :)");
                        break;
                    case KeyTransferResult.Incomplete:
                        Console.WriteLine("Seems like what you have tried has been aborted.");
                        break;
                    case KeyTransferResult.Errored:
                        Console.WriteLine("Seems like an error happened :(. Please send an email, if you want - admin@alightservices.com / kantikalyan.arumilli@alightservices.com / kantikalyan@outlook.com / kantikalyan@gmail.com");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    return;
}

IKeyTransferHelper ShowLevel1Menu(IEnumerable<IKeyTransferHelper> helpers)
{
    Console.WriteLine("Please select the use-case:");
    var count = 1;
    foreach (var helper in helpers)
    {
        Console.WriteLine($"{count}. {helper.MainText}");
        count++;
    }
    Console.WriteLine($"{count}, End");

    Console.WriteLine("Please enter your selection:");
    var response = Console.ReadLine();

    if (Int32.TryParse(response, out int responseIndex))
    {
        if (responseIndex == count)
        {
            return null;
        }
        if (responseIndex < 1 || responseIndex > count)
        {
            Console.WriteLine("Invalid input, please enter valid selection");
            ShowLevel1Menu(helpers);
        }
        return helpers.ElementAt(responseIndex - 1);
    }
    else
    {
        Console.WriteLine("Invalid input, please enter valid selection");
        ShowLevel1Menu(helpers);
    }

    return null;
}

void PrintITStartupInfo()
{
    Console.WriteLine("LightKeysTransfer version 0.0.1");
    Console.WriteLine("A product of ALight Technology And Services Limited, United Kingdom & ALight Technologies USA Inc, United States of America");
}
