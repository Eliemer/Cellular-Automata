module Tests

open Expecto
open Domain
open Console.Utils

[<Tests>]
let domainTests =
    testList
        "Domain Tests"
        [
            test "Sanity check" { Expect.equal true true "something is horribly wrong!" }

            test "Alive Cells with 2 or 3 neighbours stays alive" {
                let currentCell = CellState.Alive
                let neighbours = 2uy
                let next = Domain.evolutionRules currentCell neighbours
                Expect.equal next CellState.Alive "Cell should remain alive"

                let neighbours = 3uy
                let next = Domain.evolutionRules currentCell neighbours
                Expect.equal next CellState.Alive "Cell should remain alive"
            }
        ]

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
                    Grid [| [| CellState.Alive; CellState.Dead |]
                            [| CellState.Dead; CellState.Alive |]
                            [| CellState.Dead; CellState.Dead |] |]

                Expect.equal (printGrid printCell xs) "* \n *\n  " "Incorrect grid representation"
        ]
