module Tests

open Expecto
open Domain
open Console.Utils

[<Tests>]
let domainTests =
    testList
        "Domain Tests"
        [ test "Sanity check" { Expect.equal true true "something is horribly wrong!" }
          testProperty "Cell string representation"
          <| fun (cell: CellState) ->
              match cell with
              | CellState.Alive -> printCell cell = "*"
              | CellState.Dead -> printCell cell = " "
              | _ -> true ]

[<Tests>]
let consoleTests =
    testList
        "Console Tests"
        [ testCase "Row representation"
          <| fun () ->
              let xs =
                  [| CellState.Alive
                     CellState.Dead
                     CellState.Alive
                     CellState.Alive
                     CellState.Dead |]

              Expect.equal (printRow xs) "* ** " "Representation of row does not work as intended"

          testCase "Grid representation"
          <| fun () ->
              let xs =
                  Grid [| [| CellState.Alive; CellState.Dead |]
                          [| CellState.Dead; CellState.Alive |]
                          [| CellState.Dead; CellState.Dead |] |]

              Expect.equal (xs.ToString()) "* \n *\n  " "Incorrect grid representation" ]
