open System

open Console
open Microsoft.FSharp.Core.LanguagePrimitives
open Domain

let rand = System.Random()

[<EntryPoint>]
let main argv =
    async {
        let width, height = 32, 32

        let mutable grid =
            [|
                for w in 0..width do
                    Array.init height (fun _ ->
                        rand.Next() % 2
                        |> Convert.ToByte
                        |> EnumOfValue<byte, CellState>)
            |]
            |> Grid

        while true do
            Console.Clear()
            printfn "%s" <| printGrid Console.printCell grid

            grid <- evolveGrid grid
            do! Async.Sleep 250

    }
    |> Async.RunSynchronously

    0
