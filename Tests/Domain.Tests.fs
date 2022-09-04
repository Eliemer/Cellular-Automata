module Tests

open Expecto
open Domain
open Console

[<Tests>]
let domainTests =
    testList
        "Domain Tests"
        [
            test "Sanity check" { Expect.equal true true "something is horribly wrong!" }

            testProperty "Individual Cell evolution"
            <| fun (n : byte) ->
                let wasAlive = Domain.evolutionRules CellState.Alive n
                let wasDead = Domain.evolutionRules CellState.Alive n

                match n with
                | 2uy ->
                    Expect.equal wasAlive CellState.Alive "Alive Cell with 2 neighbours should remain alive"
                    Expect.equal wasDead CellState.Dead "Dead Cell with 2 neighbours stays dead"
                | 3uy ->
                    Expect.equal wasAlive CellState.Alive "Alive Cell with 3 neighbours should remain alive"
                    Expect.equal wasDead CellState.Alive "Dead cell with 3 neighbours should live"
                | _ ->
                    Expect.equal wasAlive CellState.Dead "Alive Cell without 2 or 3 neighbours will die"
                    Expect.equal wasDead CellState.Dead "Dead cell without 3 neighbours should remain dead"

            test "Grid Evolution" {
                let current =
                    [|
                        [| CellState.Alive; CellState.Dead |]
                        [| CellState.Dead; CellState.Dead |]
                    |]
                    |> Grid

                let next =
                    [|
                        [| CellState.Dead; CellState.Dead |]
                        [| CellState.Dead; CellState.Dead |]
                    |]
                    |> Grid

                Expect.equal next (evolveGrid current) "A single cell should die out"
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
