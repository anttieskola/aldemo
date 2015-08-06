import bb = Backbone;

// messasing hub so modules cans send messages to each other
// (circular reference between modules is forbidden)
export class MessasingHub extends bb.View<bb.Model> {
    constructor() {
        super();
        // add events you want to send across modules
        this.events = <any>
        {
            "loadComplete": "loadComplete"
        };
    }
}
export function Messaging(): MessasingHub {
    return messaging;
}
var messaging = new MessasingHub;
// collection container
export class CollectionContainer {
    private loadCounter: number;
    private static collectionCount: number = 3;
    private lines: Lines;
    private projects: Projects;
    private statuses: Statuses;
    constructor() {
        this.loadCounter = 0;
        this.lines = new Lines;
        this.lines.on('sync', this.collectionLoaded, this);
        this.lines.fetch();
        this.projects = new Projects;
        this.projects.on('sync', this.collectionLoaded, this);
        this.projects.fetch();
        this.statuses = new Statuses;
        this.statuses.on('sync', this.collectionLoaded, this);
        this.statuses.fetch();
    }
    public get Lines(): Lines {
        return this.lines;
    }
    public get Projects(): Projects {
        return this.projects;
    }
    public get Statuses(): Statuses {
        return this.statuses;
    }
    private collectionLoaded() {
        this.loadCounter++; // increment load count
        if (this.loadCounter == CollectionContainer.collectionCount) {
            Messaging().trigger("loadComplete");
        }
    } 
}
// global access to collections
export function Collections(): CollectionContainer {
    return collections;
}
// interfaces
export interface ILine {
    Name: string;
}
export interface IProject {
    Name: string;
}
export interface IStatus {
    Text: string;
    ProjectId: number;
    LineId: number;
}

// classes
export class Line extends bb.Model implements ILine {
    constructor(options?: any) {
        this.urlRoot = "/api/lines";
        super(options);
    }
    get Name(): string {
        return this.get('Name');
    }
    set Name(value: string) {
        this.set('Name', value);
    }
}
export class Project extends bb.Model implements IProject {
    constructor(options?: any) {
        this.urlRoot = "/api/projects";
        super(options);
    }
    get Name(): string {
        return this.get('Name');
    }
    set Name(value: string) {
        this.set('Name', value);
    }
} 
export class Status extends bb.Model implements IStatus {
    constructor(options?: any) {
        this.urlRoot = "/api/status";
        super(options);
    }
    get Text(): string {
        return this.get('Text');
    }
    set Text(value: string) {
        this.set('Text', value);
    }
    get ProjectId(): number {
        return this.get('ProjectId');
    }
    set ProjectId(value: number) {
        this.set('ProjectId', value);
    }
    get LineId(): number {
        return this.get('LineId');
    }
    set LineId(value: number) {
        this.set('LineId', value);
    }
}

// collections
export class Lines extends bb.Collection<Line> {
    constructor(options?: any) {
        this.url = "/api/lines";
        this.model = Line;
        super(options);
    }
}
export class Projects extends bb.Collection<Project> {
    constructor(options?: any) {
        this.url = "/api/projects";
        this.model = Project;
        super(options);
    }
}
export class Statuses extends bb.Collection<Status> {
    constructor(options?: any) {
        this.url = "/api/status";
        this.model = Status;
        super(options);
    }
    // get only given projects statues
    public ByProjectId(id: number) {
        return new Statuses(this.filter(s => s.ProjectId == id));
    }
}

// global collections
var collections = new CollectionContainer;
