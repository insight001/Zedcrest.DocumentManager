# Zedcrest Document Manager
 This project contains API endpoint that enables user to upload documents to a cloud storage server, retrieve a reference number to access documents for later use.
## Tools
1. Dotnet core 3.1
2. Docker
3. EntityFramework core
4. RabbitMQ  + MassTransit

## Setup

To run the code locally, ensure that you have docker installed, and your rabbitmq server started.

Set all configuration variables:

| Key | Description |
| ----------- | ----------- |
| DB_CONNECTION_STRING | The database connection string |
| Q.HostName  | Rabbit MQ host name
|Q.Username | RabbitMQ Username
|Q.Password | RabbitMQ Password
|Q.QueueName | Queue Name|
|Current_Email_Provider | SendGridOperation or MailGunOperation
|AZURE_STORAGE_CONNECTION | Connection string for the file storage
|AZURE_CONTAINER_NAME |The file storage container name
|SENDGRID_KEY | The Api key for the email provider

## Running the test.

1. Add 2 documents  ``` a.pdf and b.jpeg ```  
2. ``` a.pdf ``` is greater than 2MB and ``` b.jpeg ``` is smaller than or equal to 2MB