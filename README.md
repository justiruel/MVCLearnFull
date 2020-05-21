# MVCLearnFull
- Pusat layout ada di _ViewStart.cshtml -> /shared/_Layout.cshtml
- @RenderBody() menampilkan sub view dari /views/[controllerName]/[methodName]
- @RenderSection("Scripts", required: false) menampilkan dari /views/[controllerName]/[methodName]
```
@section scripts {
    <script type="text/javascript">alert('hello');</script>
}
```
- menentukan default endpoint route maksudnya http://endpoint/ lari kemana? buka Startup.cs
```
app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
```