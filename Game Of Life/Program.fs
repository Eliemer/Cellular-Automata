open System

open Console
open Domain
open Ruleset.GameOfLife

let rand = System.Random()

[<EntryPoint>]
let main argv =
    let arguments = cliParser.ParseCommandLine(raiseOnUsage = false)
    Console.Title <- "Game of Life!"

    let oldWindowSize = Console.WindowHeight, Console.WindowWidth
    let oldBufferSize = Console.BufferHeight, Console.BufferWidth

    if arguments.IsUsageRequested then
        printfn "%s" <| cliParser.PrintUsage()
    else
        async {
            let height = arguments.GetResult(CliArguments.Height, defaultValue = 32)
            let width = arguments.GetResult(CliArguments.Width, defaultValue = 64)

            do Console.Clear()
            Console.SetWindowSize(width + 10, height + 5)
            Console.SetBufferSize(width + 10, height + 5)

            let mutable grid =
                Array2D.init height width (fun _ _ ->
                    match rand.Next() % 2 = 0 with
                    | true -> CellState.Dead
                    | false -> CellState.Alive)
                |> Grid

            // // Glider!!
            // let mutable arr = Array2D.init height width (fun i j -> CellState.Dead)

            // do arr.[0, 0] <- CellState.Alive
            // do arr.[0, 2] <- CellState.Alive
            // do arr.[1, 1] <- CellState.Alive
            // do arr.[1, 2] <- CellState.Alive
            // do arr.[2, 1] <- CellState.Alive

            // let mutable grid = arr |> Grid

            let mutable oldGridString = printGrid Console.printCell grid
            let mutable stale = false

            while not stale do
                Console.Clear()

                // evolve
                let newGrid = evolveGrid evolutionRules grid
                assert (newGrid <> grid)

                // print grid
                let newGridString = printGrid Console.printCell newGrid
                printfn "%s" newGridString

                // check for 1-turn staleness
                stale <- oldGridString = newGridString

                // move new grid to old grid
                grid <- newGrid
                oldGridString <- newGridString

                do! Async.Sleep 250

            Console.SetWindowSize(oldWindowSize)
            Console.SetBufferSize(oldBufferSize)
            printfn "\nGame Over!\nThe grid has become completely stationary"
            let _ = Console.ReadLine()

            ()
        }
        |> Async.RunSynchronously

    0
