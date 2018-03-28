namespace Counter

open WebSharper
open WebSharper.UI
open WebSharper.UI.Notation
open WebSharper.UI.Templating

[<JavaScript>]
module Client =
    // The templates are loaded from the DOM, so you just can edit index.html
    // and refresh your browser, no need to recompile unless you add or remove holes.
    type MySPA = Template<"wwwroot/index.html", ClientLoad.FromDocument>

    type Model = int

    type Message =
        | Increment
        | Decrement

    let update msg model =
        match msg with
        | Message.Increment ->
            model+1
        | Message.Decrement ->
            model-1

    let init = 0

    let view =
        let vmodel = Var.Create init
        let handle msg =
            let model = update msg vmodel.Value
            vmodel := model
        MySPA()
            .OnIncrement(fun _ -> handle Message.Increment)
            .OnDecrement(fun _ -> handle Message.Decrement)
            .Counter(V(string vmodel.V))
            .Bind()
        fun model ->
            vmodel := model

    [<SPAEntryPoint>]
    let Main () =
        view init
