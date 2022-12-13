namespace Mihaiu_Ionut_Lab7;

using Mihaiu_Ionut_Lab7.Data;
using Mihaiu_Ionut_Lab7.Models;

public partial class ListPage : ContentPage
    
{
    public ListPage()
    {
        InitializeComponent();
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)
       this.BindingContext)
        {
            BindingContext = new Product()
        });

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (Product)BindingContext;
        await App.Database.DeleteProductAsync(slist);
        listView.ItemsSource = await App.Database.GetProductsAsync();
    }
    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        Product product;
        var shopList = (ShopList)BindingContext;
        if (listView.SelectedItem != null)
        {
            product = listView.SelectedItem as Product;
            var listProductAll = await App.Database.GetListProducts();
            var listProduct = listProductAll.FindAll(x => x.ProductID == product.ID & x.ShopListID == shopList.ID);
            await App.Database.DeleteListProductAsync(listProduct.FirstOrDefault());
            await Navigation.PopAsync();
        }

    }
}
    
           
       
