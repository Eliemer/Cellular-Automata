module Domain

open Shared

type Grid<'CellState when 'CellState : enum<char>> = Grid of 'CellState [,]

let mooreNeighbourhood ((row, col) : int * int) ((h, w) : int * int) : seq<int * int> =
    [
        (row - 1, col - 1)
        (row - 1, col)
        (row - 1, col + 1)
        (row, col - 1)
        // (i, j) don't consider self
        (row, col + 1)
        (row + 1, col - 1)
        (row + 1, col)
        (row + 1, col + 1)
    ]
    |> Seq.map (fun (i, j) -> (i %! h), (j %! w))

let evolveCell<'CellState when 'CellState : enum<char>>
    (rules : 'CellState -> 'CellState seq -> 'CellState)
    ((i, j) : int * int)
    (cell : 'CellState)
    (Grid grid : Grid<'CellState>)
    : 'CellState =

    mooreNeighbourhood (i, j) (Array2D.length1 grid, Array2D.length2 grid)
    |> Seq.map (fun (i, j) -> grid.[i, j])
    |> (rules cell)

let evolveGrid<'CellState when 'CellState : enum<char>>
    (rules : 'CellState -> 'CellState seq -> 'CellState)
    (Grid grid : Grid<'CellState>)
    : Grid<'CellState> =

    grid
    |> Array2D.mapi (fun i j cell -> evolveCell rules (i, j) cell (Grid grid))
    |> Grid
