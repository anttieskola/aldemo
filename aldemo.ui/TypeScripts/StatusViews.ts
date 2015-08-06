import bb = Backbone;
import models = require("Models");

export class StatusView extends bb.View<models.Status> {
    private template: (data: any) => string;
    constructor(model: models.Status) {
        // model
        this.model = model;
        // template from index.html
        this.template = _.template($('#status-template').html());
        super();
    }
    render(): bb.View<models.Status> {
        this.$el.html(this.template(this.model.toJSON()));
        // events
        this.undelegateEvents();
        this.delegateEvents({
            "contextmenu .statusDiv": "openMenu"
        });
        return this;
    }
    // context menu
    openMenu(event) {
        // prevent normal context menu popping up (might not be cross-browser)
        event.preventDefault();
        // find cursor position
        var left = event.clientX;
        var top = event.clientY;
        // open menu at position
        var menu = $('#menu');
        menu.css('left', left);
        menu.css('top', top);
        menu.show();
        // need to hook-up to menu, note using proxy so context stays inside class
        menu.on('mouseout', $.proxy(this.moveOut, this));
        menu.on('click', $.proxy(this.clickMenu, this));
    }
    // mouse moving outside menu
    moveOut(event) {
        // need to check is mouse in one of the child elements of menu
        var menu = $('#menu');
        var current = $(event.toElement);
        if (menu.find(current).length < 1) {
            this.closeMenu();
        }
    }
    // click in the menu
    clickMenu(event) {
        var e = $(event.toElement);
        var newColor = e.text();
        this.model.Text = newColor;
        this.model.save();
        this.render();
        this.closeMenu();
    }
    // close menu
    closeMenu() {
        var menu = $('#menu');
        // it is outside children, so hide
        menu.hide();
        // un-hook
        menu.off('mouseout', this.closeMenu);
        menu.off('click', this.clickMenu);
    }
}
