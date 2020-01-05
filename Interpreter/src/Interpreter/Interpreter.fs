/// Объекты интерпретатора лямбда-выражений.
module Interpreter

open Normalizer

/// Переменная
let (!) value = Variable(value)

/// Лямбда-абстракция
let (+) name term = LambdaAbstraction(name, term)

/// Аппликация
let (*) term1 term2 = Application(term1, term2)