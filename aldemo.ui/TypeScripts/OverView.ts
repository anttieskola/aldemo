import bb = Backbone;
import models = require("Models");
import statusViews = require("StatusViews");

// overview has no real model so making it a normal class
export class OverView {
    // dom element to draw upon (naming it same way as backbone)
    private el: JQuery;
    // we need collections of projects, lines and status on those lines
    private projects: models.Projects;
    private lines: models.Lines;
    private statuses: models.Statuses;

    // constructor
    constructor(elementId: string) {
        // set element to draw upon
        this.el = $('#' + elementId);
        // render when all collections loaded
        models.Messaging().on("loadComplete", this.render, this);
    }

    // render view (naming like backbone)
    public render() {
        var table = $('<table>');
        var row = $('<tr style="height: 125px;">');
        row.append($('<td>')); // empty colum for project name
        models.Collections().Lines.each(l => {
            var cell = $('<td style="-webkit-transform: rotate(-90deg); max-width: 25px;">');
            cell.attr('data-line-id', l.id);
            cell.text(l.Name);
            row.append(cell);
        }, this);
        table.append(row);

        models.Collections().Projects.each(p => {
            // row and project name
            var tr = $('<tr>');
            tr.attr('data-project-id', p.id);
            var td = $('<td>');
            td.text(p.Name);
            tr.append(td);
            // status
            models.Collections().Statuses.ByProjectId(p.id).each(s => {
                var td = $('<td>');
                var sv = new statusViews.StatusView(s);
                td.append(sv.render().el);
                tr.append(td);
            }, this);
            // add row to table
            table.append(tr);
        }, this);
        this.el.html(""); // clean (when all loading ready)
        this.el.append(table); // add
    }
}
