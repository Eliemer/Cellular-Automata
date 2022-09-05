module Argu.Tests

open Argu
open Console
open Expecto

[<Tests>]
let arguTests =
    testList
        "Argu Tests"
        [
            test "CliArguments structure check" { ArgumentParser<CliArguments>.CheckStructure () }
        ]
