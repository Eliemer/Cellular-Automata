module Console.Utils

open System
open Domain

let printCell (cell : CellState) =
    match cell with
    | CellState.Alive -> "*"
    | CellState.Dead -> " "
    | _ -> ""

let printRow (printCell : CellState -> string) (row : CellState []) =
    row |> Seq.map printCell |> String.concat ""

let printGrid (cellPrinter : CellState -> string) (Grid grid) =
    grid
    |> Seq.map (printRow printCell)
    |> String.concat "\n"
