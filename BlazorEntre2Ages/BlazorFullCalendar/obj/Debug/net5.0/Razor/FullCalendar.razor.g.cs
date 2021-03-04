#pragma checksum "F:\Users\camsl\Source\Repos\Projet Entre2Ages\BlazorEntre2Ages\BlazorFullCalendar\FullCalendar.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e9d4df65909a98cba27b4199d0811192ecf73190"
// <auto-generated/>
#pragma warning disable 1591
namespace BlazorFullCalendar
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "F:\Users\camsl\Source\Repos\Projet Entre2Ages\BlazorEntre2Ages\BlazorFullCalendar\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\Users\camsl\Source\Repos\Projet Entre2Ages\BlazorEntre2Ages\BlazorFullCalendar\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "F:\Users\camsl\Source\Repos\Projet Entre2Ages\BlazorEntre2Ages\BlazorFullCalendar\_Imports.razor"
using Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "F:\Users\camsl\Source\Repos\Projet Entre2Ages\BlazorEntre2Ages\BlazorFullCalendar\_Imports.razor"
using Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "F:\Users\camsl\Source\Repos\Projet Entre2Ages\BlazorEntre2Ages\BlazorFullCalendar\_Imports.razor"
using Newtonsoft.Json;

#line default
#line hidden
#nullable disable
    public partial class FullCalendar : Microsoft.AspNetCore.Components.ComponentBase, IDisposable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "id", 
#nullable restore
#line 5 "F:\Users\camsl\Source\Repos\Projet Entre2Ages\BlazorEntre2Ages\BlazorFullCalendar\FullCalendar.razor"
                      Id

#line default
#line hidden
#nullable disable
            );
            __builder.AddMultipleAttributes(2, Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<string, object>>>(
#nullable restore
#line 5 "F:\Users\camsl\Source\Repos\Projet Entre2Ages\BlazorEntre2Ages\BlazorFullCalendar\FullCalendar.razor"
                                        ExtraAttributes

#line default
#line hidden
#nullable disable
            ));
            __builder.AddElementReferenceCapture(3, (__value) => {
#nullable restore
#line 5 "F:\Users\camsl\Source\Repos\Projet Entre2Ages\BlazorEntre2Ages\BlazorFullCalendar\FullCalendar.razor"
           elem = __value;

#line default
#line hidden
#nullable disable
            }
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(4, "\r\n\r\n");
            __Blazor.BlazorFullCalendar.FullCalendar.TypeInference.CreateCascadingValue_0(__builder, 5, 6, 
#nullable restore
#line 8 "F:\Users\camsl\Source\Repos\Projet Entre2Ages\BlazorEntre2Ages\BlazorFullCalendar\FullCalendar.razor"
                        this

#line default
#line hidden
#nullable disable
            , 7, (__builder2) => {
                __builder2.AddMarkupContent(8, "\r\n    ");
                __builder2.AddContent(9, 
#nullable restore
#line 9 "F:\Users\camsl\Source\Repos\Projet Entre2Ages\BlazorEntre2Ages\BlazorFullCalendar\FullCalendar.razor"
     ChildContent

#line default
#line hidden
#nullable disable
                );
                __builder2.AddMarkupContent(10, "\r\n");
            }
            );
        }
        #pragma warning restore 1998
#nullable restore
#line 12 "F:\Users\camsl\Source\Repos\Projet Entre2Ages\BlazorEntre2Ages\BlazorFullCalendar\FullCalendar.razor"
       
    ElementReference elem;

    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> ExtraAttributes { get; set; }
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter] public string Id { get; set; } = "calendar";
    [Parameter] public CalendarSettings settings { get; set; }
    [Parameter] public EventCallback<CalendarEventChangeResponse> OnAddEvent { get; set; }
    [Parameter] public EventCallback<CalendarEventChangeResponse> OnUpdateEvent { get; set; }
    [Parameter] public EventCallback<CalendarEventChangeResponse> OnClickEvent { get; set; }

    private CalendarInteropService interop;
    private DotNetObjectReference<FullCalendar> _objRef;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            interop = new CalendarInteropService(jsRuntime);
            _objRef = DotNetObjectReference.Create(this);
            await interop.SetDotNetReference(_objRef);
            await InitCalendar();
        }
    }

    public async Task InitCalendar()
    {
        await interop.CalendarInit("calendar", settings);
        await InvokeAsync(() => { StateHasChanged(); });
    }

    public async Task ChangeResourceFeed(CalendarResourceFeed calendarResourceFeed)
    {
        //var interop = new Services.InteropService(jsRuntime);
        await interop.CalendarChangeResourceFeed(calendarResourceFeed);
        await InvokeAsync(() => { StateHasChanged(); });
    }

    public async Task CalendarRefetchEvents()
    {
        await interop.CalendarRefetchEvents();
        await InvokeAsync(() => { StateHasChanged(); });
    }

    public async Task ChangeSlotWidth(int value)
    {
        await interop.CalendarSetOption("slotWidth", value);
        await InvokeAsync(() => { StateHasChanged(); });
    }

    [JSInvokable("AddEventCallback")]
    public async Task AddEventCallback(string returnValue)
    {
        try
        {
            var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
            await OnAddEvent.InvokeAsync(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    [JSInvokable("UpdateEventCallback")]
    public async Task UpdateEventCallback(string returnValue)
    {
        try
        {
            var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
            await OnUpdateEvent.InvokeAsync(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    [JSInvokable("ClickEventCallback")]
    public async Task ClickEventCallback(string returnValue)
    {
        try
        {
            var response = JsonConvert.DeserializeObject<CalendarEventChangeResponse>(returnValue);
            await OnClickEvent.InvokeAsync(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Dispose()
    {
        _objRef?.Dispose();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime jsRuntime { get; set; }
    }
}
namespace __Blazor.BlazorFullCalendar.FullCalendar
{
    #line hidden
    internal static class TypeInference
    {
        public static void CreateCascadingValue_0<TValue>(global::Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder, int seq, int __seq0, TValue __arg0, int __seq1, global::Microsoft.AspNetCore.Components.RenderFragment __arg1)
        {
        __builder.OpenComponent<global::Microsoft.AspNetCore.Components.CascadingValue<TValue>>(seq);
        __builder.AddAttribute(__seq0, "Value", __arg0);
        __builder.AddAttribute(__seq1, "ChildContent", __arg1);
        __builder.CloseComponent();
        }
    }
}
#pragma warning restore 1591
