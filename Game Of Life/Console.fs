module Console

open System
open Domain
open Ruleset.GameOfLife
open Argu

let toArrayOfArrays arr =
    let ($) (bas, len) f = Array.init len (fun n -> bas + n |> f)

    (Array2D.base1 arr, Array2D.length1 arr)
    $ fun x ->
        (Array2D.base2 arr, Array2D.length2 arr)
        $ fun y -> arr.[x, y]

let printCell (cell : CellState) =
    LanguagePrimitives.EnumToValue cell |> string

let printRow (cellPrinter : CellState -> string) (row : CellState []) =
    row |> Seq.map cellPrinter |> String.concat ""

let printGrid (cellPrinter : CellState -> string) (Grid grid) =
    grid
    |> toArrayOfArrays
    |> Seq.map (printRow cellPrinter)
    |> String.concat "\n"

type CliArguments =
    | [<Unique; AltCommandLine("-h")>] Height of int
    | [<Unique; AltCommandLine("-w")>] Width of int

    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Height _ -> "specify the height of the grid"
            | Width _ -> "specify the width of the grid"

let cliParser =
    ArgumentParser.Create<CliArguments>(programName = "game_of_life.exe")
