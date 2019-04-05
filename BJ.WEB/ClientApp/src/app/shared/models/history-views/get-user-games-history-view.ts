export class GetUserGamesHistoryView {

    public games: Array<GamesGetUserGamesHistoryViewItem>

    /**
     *
     */
    constructor() {
        this.games = [];

    }

}

export class GamesGetUserGamesHistoryViewItem {
    public gameId: string;
    public dateTime: Date;
    public countBots: number;
    public winner: string;
}
