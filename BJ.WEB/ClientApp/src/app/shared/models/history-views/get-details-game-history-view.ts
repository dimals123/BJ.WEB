import { RankTypeView } from '../enums-views/rank-type-view';
import { SuitTypeView } from '../enums-views/suit-type-view';

export class GetDetailsGameHistoryView {


    public dateTime: Date;
    public countBots: number;
    public winner: string;

    public user: UserGetDetailsGameHistoryView;
    public bots: Array<BotGetDetailsGameHistoryViewItem>;
   
    /**
     *
     */
    constructor() {
        this.bots = [];
    }
}

export class UserGetDetailsGameHistoryView {
    public name: string;
    public points: number;
    public cards: Array<StepUserGetDetailsGameHistoryViewItem>;

    constructor() {
        this.cards = [];
    }

}

export class StepUserGetDetailsGameHistoryViewItem {
    public suit: SuitTypeView;
    public rank: RankTypeView;
}

export class BotGetDetailsGameHistoryViewItem {
    public name: string;
    public points: number;
    public cards: Array<StepBotGetDetailsGameHistoryViewItem>;

    constructor() {
        this.cards = [];
    }
}

export class StepBotGetDetailsGameHistoryViewItem {
    public suit: SuitTypeView;
    public rank: RankTypeView;
}
