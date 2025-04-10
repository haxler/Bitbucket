using Bitbucket.Frontend.Repositories;
using Bitbucket.Frontend.Shared;
using Bitbucket.Shared.Entities;
using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;

namespace Bitbucket.Frontend.Pages.Products;

public partial class ProductCreate
{
    private FormWithName<Product>? productForm;
    private Product product = new();

    [Inject] private IRepository Repository { get; set; } = null!;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
    [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;

    private async Task CreateAsync()
    {
        var responseHttp = await Repository.PostAsync("/api/products", product);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            await SweetAlertService.FireAsync("Error", message);
            return;
        }
        Return();

        var toast = SweetAlertService.Mixin(new SweetAlertOptions
        {
            Toast = true,
            Position = SweetAlertPosition.BottomEnd,
            ShowConfirmButton = true,
            Timer = 3000
        });
        await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
    }

    private void Return()
    {
        productForm!.FormPostedSuccessfully = true;
        NavigationManager.NavigateTo("/products");
    }
}