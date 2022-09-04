module Domain

type CellState =
    | Dead = 0uy
    | Alive = 1uy

type Grid = Grid of CellState [] []

let evolutionRules (cell : CellState) (neighbours : byte) =
    match cell, neighbours with
    | CellState.Dead, 3uy -> CellState.Alive
    | CellState.Alive, 2uy
    | CellState.Alive, 3uy -> CellState.Alive
    | _ -> CellState.Dead

let getAdjacent ((i, j) : int * int) (Grid grid) =
    let h = grid.Length
    let w = grid.[0].Length

    [
        (i - 1, j - 1)
        (i - 1, j)
        (i - 1, j + 1)
        (i, j - 1)
        // (i, j) don't consider self
        (i, j + 1)
        (i + 1, j - 1)
        (i + 1, j)
        (i + 1, j + 1)
    ]
    |> Seq.map (fun (i, j) -> (i % h), (j % w))
    |> Seq.map (fun (i, j) -> grid.[i].[j])

let evolveCell ((i, j) : int * int) (cell : CellState) (Grid grid) : CellState =
    getAdjacent (i, j) (Grid grid)
    |> Seq.sumBy<CellState, byte> byte
    |> (evolutionRules cell)

let evolveGrid (Grid grid) : Grid =
    grid
    |> Array.mapi (fun i row ->
        row
        |> Array.mapi (fun j cell -> evolveCell (i, j) cell (Grid grid)))
    |> Grid
