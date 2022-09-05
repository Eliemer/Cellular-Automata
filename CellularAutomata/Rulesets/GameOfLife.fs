module Ruleset.GameOfLife

type CellState =
    | Dead = ' '
    | Alive = '█'

let evolutionRules (cell : CellState) (neighbours : CellState seq) =

    let numOfNeighbours =
        neighbours
        |> Seq.filter ((=) CellState.Alive)
        |> Seq.length

    match cell, numOfNeighbours with
    | CellState.Dead, 3 -> CellState.Alive
    | CellState.Alive, 2
    | CellState.Alive, 3 -> CellState.Alive
    | _ -> CellState.Dead
