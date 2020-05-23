# MVCLearnFull

## Pusat layout ada di _ViewStart.cshtml -> /shared/_Layout.cshtml

## @RenderBody() menampilkan sub view dari /views/[controllerName]/[methodName]

## @RenderSection("Scripts", required: false) menampilkan dari /views/[controllerName]/[methodName]
```
@section scripts {
    <script type="text/javascript">alert('hello');</script>
}
```

## @ViewData["Title"] apa bedanya dengan viewbag, sama saja penggunaanya ref : 
- https://www.it-swarm.dev/id/.net/apa-perbedaan-antara-viewdata-dan-viewbag/971980826/
- https://www.it-swarm.dev/id/c%23/viewbag-viewdata-tempdata-session-bagaimana-dan-kapan-menggunakannya/1070836750/
- https://stackoverflow.com/questions/12422930/using-tempdata-in-asp-net-mvc-best-practice

## menentukan default endpoint route maksudnya http://endpoint/ lari kemana? buka Startup.cs
```
app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
```

## Session
- https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-3.1
