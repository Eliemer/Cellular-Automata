open System

open Console
open Microsoft.FSharp.Core.LanguagePrimitives
open Domain

let rand = System.Random()

[<EntryPoint>]
let main argv =
    let width, height = 32, 32

    let exampleGrid =
        [|
            for w in 0..width do
                Array.init height (fun _ ->
                    rand.Next() % 2
                    |> Convert.ToByte
                    |> EnumOfValue<byte, CellState>)
        |]
        |> Grid

    printfn "%s" <| exampleGrid.ToString()
    Console.Read() |> ignore
    0
