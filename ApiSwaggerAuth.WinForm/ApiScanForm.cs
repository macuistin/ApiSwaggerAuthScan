using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApiSwaggerAuth.Domain.Entities;
using ApiSwaggerAuth.Domain.Services;

namespace ApiSwaggerAuth.WinForm
{
    public partial class ApiScanForm : Form
    {
        private readonly IApiService _apiService;

        public ApiScanForm(IApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                btnScan.Enabled = false;

                await ScanAndOutput(txtApiUrl.Text);
            }
            catch (Exception ex)
            {
                txtResults.Text = ex.ToString();
            }
            finally
            {
                btnScan.Enabled = true;
            }
        }

        private async Task ScanAndOutput(string url)
        {
            txtResults.Text = "Running...";

            var data = await _apiService.GetAsync(url);

            if (data == null)
            {
                txtResults.Text = "What happened?";
            }

            var output = GetDataOutput(data);
            txtResults.Text = string.Empty;
            await foreach(var result in output)
            {
                txtResults.AppendText(result);
            }
        }

        private static async IAsyncEnumerable<string> GetDataOutput(ApiData data)
        {

            yield return $"Url:\t{data.ApiUrl}\r\n";
            yield return $"Status:\t{data.Status}\r\n";
            yield return $"Probe:\t{data.ProbeStatus}\r\n";

            if (data.ProbeStatus == System.Net.HttpStatusCode.OK)
            {
                yield return "Swagger Docs:\r\n";
                await foreach (var swaggerResult in data.SwaggerResults)
                {
                    yield return $"\t{swaggerResult.Url} : [{swaggerResult.Name}]\r\n";
                    yield return "\tOperations:\r\n";
                    await foreach (var methodResult in swaggerResult.MethodProbeResults)
                    {
                        yield return $"\t\t{methodResult.StatusCode} :\t[{methodResult.Verb}]\t{methodResult.Path}\r\n";
                    }
                }
            }

            yield return "\r\n\t*** COMPLETE ***\r\n";

        }
    }
}
