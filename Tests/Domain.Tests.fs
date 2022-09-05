module Tests

open Expecto
open FsCheck
open Domain
open Ruleset.GameOfLife

[<Tests>]
let domainTests =
    testList
        "Game of Life -- Domain Tests"
        [
            test "Sanity check" { Expect.equal true true "something is horribly wrong!" }

            test "Moore Neighbourhood" {
                let actual = mooreNeighbourhood (3, 3) (5, 5)

                let expected =
                    seq {
                        2, 2
                        2, 3
                        2, 4
                        3, 2
                        3, 4
                        4, 2
                        4, 3
                        4, 4
                    }

                Expect.sequenceEqual actual expected "Correct neighbourhood"

            }

            testProperty "Individual Cell evolution"
            <| fun (NonNegativeInt n) ->
                let n = n % 9

                let neighbours = Seq.replicate n CellState.Alive

                let wasAlive = evolutionRules CellState.Alive neighbours
                let wasDead = evolutionRules CellState.Dead neighbours

                match n with
                | 2 ->
                    Expect.equal wasAlive CellState.Alive "Alive Cell with 2 neighbours should remain alive"
                    Expect.equal wasDead CellState.Dead "Dead Cell with 2 neighbours stays dead"
                | 3 ->
                    Expect.equal wasAlive CellState.Alive "Alive Cell with 3 neighbours should remain alive"
                    Expect.equal wasDead CellState.Alive "Dead cell with 3 neighbours should live"
                | _ ->
                    Expect.equal wasAlive CellState.Dead "Alive Cell without 2 or 3 neighbours will die"
                    Expect.equal wasDead CellState.Dead "Dead cell without 3 neighbours should remain dead"

            test "A lonely cell dies alone" {
                let current =
                    [|
                        [| CellState.Alive; CellState.Dead |]
                        [| CellState.Dead; CellState.Dead |]
                    |]
                    |> array2D
                    |> Grid

                let next =
                    [|
                        [| CellState.Dead; CellState.Dead |]
                        [| CellState.Dead; CellState.Dead |]
                    |]
                    |> array2D
                    |> Grid

                Expect.equal (evolveGrid evolutionRules current) next "A single cell should die out"
            }

            test "Blinker oscillator" {
                let current = Array2D.create 5 5 CellState.Dead
                current.[2, 1] <- CellState.Alive
                current.[2, 2] <- CellState.Alive
                current.[2, 3] <- CellState.Alive

                let next = Array2D.create 5 5 CellState.Dead
                next.[1, 2] <- CellState.Alive
                next.[2, 2] <- CellState.Alive
                next.[3, 2] <- CellState.Alive

                Expect.equal (evolveGrid evolutionRules (Grid current)) (Grid next) "Blinker oscillator is broken!"

            }

            testProperty "Everyone dies from total overpopulation"
            <| fun (NonNegativeInt h) (NonNegativeInt w) ->
                let current = Array2D.create h w CellState.Alive |> Grid
                let next = Array2D.create h w CellState.Dead |> Grid
                Expect.equal next (evolveGrid evolutionRules current) "Everyone should die from total overpopulation"

        ]
