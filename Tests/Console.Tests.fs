module Console.Tests

open Expecto
open Domain
open Console
open Ruleset.GameOfLife


[<Tests>]
let consoleTests =
    testList
        "Console Tests"
        [
            testProperty "Cell string representation"
            <| fun (cell : CellState) ->
                match cell with
                | CellState.Alive -> printCell cell = "*"
                | CellState.Dead -> printCell cell = " "
                | _ -> true

            testCase "Row representation"
            <| fun () ->
                let xs =
                    [|
                        CellState.Alive
                        CellState.Dead
                        CellState.Alive
                        CellState.Alive
                        CellState.Dead
                    |]

                Expect.equal (printRow printCell xs) "* ** " "Representation of row does not work as intended"

            testCase "Grid representation"
            <| fun () ->
                let xs =
                    [|
                        [| CellState.Alive; CellState.Dead |]
                        [| CellState.Dead; CellState.Alive |]
                        [| CellState.Dead; CellState.Alive |]
                    |]
                    |> (array2D >> Grid)

                Expect.equal (printGrid printCell xs) "* \n *\n *" "Incorrect grid representation"
        ]
