module Domain

type CellState =
    | Dead = 0uy
    | Alive = 1uy

let printCell (cell: CellState) =
    match cell with
    | CellState.Alive -> "*"
    | CellState.Dead -> " "
    | _ -> ""
