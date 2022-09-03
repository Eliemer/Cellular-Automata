module Console.Utils

open System
open Domain

let printRow (row: CellState []) =
    row |> Seq.map printCell |> String.concat ""

type Grid =
    | Grid of CellState [] []

    override this.ToString() =
        match this with
        | Grid grid -> grid |> Seq.map printRow |> String.concat "\n"
