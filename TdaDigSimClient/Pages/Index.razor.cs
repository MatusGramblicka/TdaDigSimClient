using Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TdaDigSimClient.HttpRepository;
using TdaDigSimClient.Serializers;

namespace TdaDigSimClient.Pages
{
    //https://romansimuta.com/posts/drag-and-drop-file-uploading-with-net-5-0-blazor-inputfile-component/
    //https://wellsb.com/csharp/aspnet/drag-drop-file-upload-blazor/
    //https://docs.microsoft.com/en-us/aspnet/core/blazor/file-uploads?view=aspnetcore-5.0&pivots=webassembly
    public partial class Index
    {
        const int MaxFileSizeMB = 20;
        const int MaxFileSize = MaxFileSizeMB * 1024 * 1024; // 20MB
        string dropClass = string.Empty;
        private IBrowserFile file;
        private bool fileSizeError = false;
        private bool fileTypeError = false;
        List<string> acceptedFileTypes = new List<string>() { "application/json" };

        [Inject]
        public ISimulatorHttpRepository SimulatorHttpRepository { get; set; }

        private void HandleDragEnter()
        {
            dropClass = "dropAreaDrug";
        }
        private void HandleDragLeave()
        {
            dropClass = string.Empty;
        }
        public enum RecordTypeEnum2
        {
            ForceRecord = 0,
            PresenceRecord = 1
        }
        private void LoadFiles(InputFileChangeEventArgs e)
        {
            file = null;
            fileSizeError = false;
            fileTypeError = false;
            bool error = false;

            if (e.File.Size > MaxFileSize)
            {
                error = true;
                fileSizeError = true;
            }
            if (!acceptedFileTypes.Contains(e.File.ContentType))
            {
                error = true;
                fileTypeError = true;
            }

            if (!error)
            {
                file = e.File;
            }          
        }

        private void RemoveFile(IBrowserFile fileToRemove)
        {
            fileToRemove = null;
            file = fileToRemove;
        }

        private async Task SendToSimulator(IBrowserFile fileToSend)
        {
            if (file != null)
            {
                try
                {
                    using var reader =
                        new StreamReader(file.OpenReadStream(MaxFileSize));

                    var text = await reader.ReadToEndAsync();
                    //await SimulatorHttpRepository.SendPayload("http://192.168.194.59:3001/api/sender/send", text);
                    var result = await Task.Run(() =>
                    JsonConvert.DeserializeObject<Scenario>(text, SerializationsSettingsProvider.JsonSerializerSettings()));
                    await SimulatorHttpRepository.SendPayload("http://192.168.194.59:3001/api/sender/send", result);
                }
                catch (Exception ex)
                {

                }
            }
        }        
    }
}
