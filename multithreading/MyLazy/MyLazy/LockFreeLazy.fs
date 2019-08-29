module LockFreeLazy
    
    open ILazy
    open System.Threading

    /// Многопоточная реализация для lock-free lazy-объекта.
    type LockFreeLazy<'a> (supplier : unit -> 'a) =                       
        /// Результат вычисления.
        [<DefaultValue>] val mutable result : 'a

        /// Изменяемое имя, в которое запишем результат для игнорирования второго и бОльших вычислений.
        let ignoreNextResults = ref Unchecked.defaultof<'a>
    
        /// Выполнено ли вычисление.
        let isValueCreated = ref false        

        interface ILazy<'a> with
            member this.Get () =
                if !isValueCreated then this.result
                else
                    this.result <- supplier ()
                    let actual = Interlocked.CompareExchange(ref ignoredResult, Unchecked.defaultof<'a>, this.result)
                    Volatile.Write(isValueCreated, true)
                    !ignoreNextResults <- this.result

        /// Выполнено ли вычисление.
        member this.IsValueCreated = isValueCreated