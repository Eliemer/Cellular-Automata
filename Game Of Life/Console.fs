module Console

open System
open Domain

let printCell (cell : CellState) =
    match cell with
    | CellState.Alive -> "*"
    | CellState.Dead -> " "
    | _ -> ""

let printRow (cellPrinter : CellState -> string) (row : CellState []) =
    row |> Seq.map cellPrinter |> String.concat ""

let printGrid (cellPrinter : CellState -> string) (Grid grid) =
    grid
    |> Seq.map (printRow cellPrinter)
    |> String.concat "\n"
