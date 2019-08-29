/// Создание lazy-объектов различной реализации.
module LazyFactory

    open SingleThreadedLazy
    open SyncedLazy
    open LockFreeLazy

    /// Фабрика lazy-объектов.
    type Factory() =
        /// Однопоточная реализация lazy-объекта.
        static member CreateSingleThreadedLazy (supplier : unit -> 'a) = new SingleThreadedLazy<'a>(supplier)

        /// Многопоточная реализация lazy-объекта с синхронизацией.
        static member CreateSyncedLazy (supplier : unit -> 'a) = new SyncedLazy<'a>(supplier)

        /// Многопоточная реализация lazy-объекта в lock-free стиле.
        //static member CreateLockFreeLazy supplier = new LockFreeLazy(supplier)