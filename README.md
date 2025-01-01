# Data Archival System  

The **Data Archival System** is a scalable solution designed to efficiently manage and archive data using **Azure Blob Storage**. This system enables secure file operations such as uploading, downloading, and dynamically managing blob access tiers (Hot, Cool, Archive) for cost optimization and data lifecycle management.  

---

## Features  

### 🔄 File Operations  
- **Upload Files**: Securely upload files of various formats to Azure Blob Storage.  
- **Download Files**: Retrieve stored files with reliability and efficiency.  

### 🎚 Access Tier Management  
- Dynamically update access tiers to optimize costs:  
  - **Hot**: For frequently accessed data.  
  - **Cool**: For infrequently accessed data with lower storage costs.  
  - **Archive**: For long-term storage of rarely accessed data.  

### 🌟 Scalability and Reliability  
- Leverages Azure's robust infrastructure for high availability and durability.  
- Handles large volumes of data with seamless scalability.  

### 🛠 User-Friendly Design  
- Easy-to-use interface for managing files and access tiers.  
- Detailed logging and error reporting for smooth troubleshooting.  

---

## Getting Started  

### Prerequisites  
1. An **Azure Storage Account**.  
2. Configure **Azure Blob Storage** containers for file storage.  
3. Install the required dependencies:  
   ```bash
