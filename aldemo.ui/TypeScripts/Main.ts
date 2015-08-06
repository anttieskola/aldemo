import ov = require("OverView");

export module Main {
    class Application {
        private overView: ov.OverView;
        constructor() {
            this.overView = new ov.OverView("application");
        }
    }
    export function Run() {
        var application = new Application();
    }
}
