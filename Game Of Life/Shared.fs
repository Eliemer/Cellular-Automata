module Shared

/// Euclidean remainder, the proper modulo operation
let inline (%!) a b = (a % b + b) % b
