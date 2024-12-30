using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

string connectionString = "XXXX";
string containerName = "archived-data";

BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

DataArchivalSystem archivalSystem = new(connectionString, containerName);

// Create or Get Container
BlobContainerClient containerClient = await archivalSystem.CreateContainerAsync(blobServiceClient);

Console.WriteLine("----- Select option -----");
Console.WriteLine("----- 1 - Upload File -----");
Console.WriteLine("----- 2 - Download File -----");
Console.WriteLine("----- 3 - Set Access Tier -----");


int option = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("----- Please enter file path -----");
string filePath = Console.ReadLine() ?? "sample.txt"; // Replace with your file path
string blobName = Path.GetFileName(filePath);

switch (option)
{
    case 1:
        // Upload a File
        Console.WriteLine($"Uploading '{blobName}'...");
        await archivalSystem.UploadFileAsync(containerClient, filePath);
        break;
    case 2:
        // Download a File
        Console.WriteLine($"Downloading '{blobName}'...");
        await archivalSystem.DownloadFileAsync(containerClient, filePath, "downloaded-"+filePath);
        break;
    case 3:
        // Set Access Tier
        await archivalSystem.SetAccessTierAsync(containerClient, filePath, AccessTier.Archive);        
        break;
    default:
        Console.WriteLine("Selected option not found.");
        break;
}

public class DataArchivalSystem
{
    private string connectionString;
    private string containerName;
    public DataArchivalSystem(string connectionString, string containerName){
        this.connectionString = connectionString;
        this.containerName = containerName;
    }
    
    public async Task<BlobContainerClient> CreateContainerAsync(BlobServiceClient blobServiceClient)
    {
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        if (!await containerClient.ExistsAsync())
        {
            await containerClient.CreateAsync();
            Console.WriteLine($"Container '{containerName}' created.");
        }
        else
        {
            Console.WriteLine($"Container '{containerName}' already exists.");
        }
        return containerClient;
    }

    public async Task UploadFileAsync(BlobContainerClient containerClient, string filePath)
    {
        string blobName = Path.GetFileName(filePath);
        try{
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            using FileStream uploadFileStream = File.OpenRead(filePath);
            await blobClient.UploadAsync(uploadFileStream, true);
            Console.WriteLine($"File '{blobName}' uploaded.");
        }
        catch(Exception ex){
            Console.WriteLine($"Unable to upload file '{blobName}', Error: {ex.Message}");
        }
    }

    public async Task DownloadFileAsync(BlobContainerClient containerClient, string blobName, string downloadPath)
    {
        try{
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DownloadToAsync(downloadPath);
            Console.WriteLine($"File '{blobName}' downloaded to '{downloadPath}'.");
        }
        catch(Exception ex){
            Console.WriteLine($"Unable to download file '{blobName}', Error: {ex.Message}");
        }
    }

    public async Task SetAccessTierAsync(BlobContainerClient containerClient, string blobName, AccessTier tier)
    {
        BlobClient blobClient = containerClient.GetBlobClient(blobName);
        await blobClient.SetAccessTierAsync(tier);
        Console.WriteLine($"Access tier for '{blobName}' set to '{tier}'.");
    }
}