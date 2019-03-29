export class GetDetailsGameResponseView
{
    user: UserGetDetailsGameResponseView;
    bots: Array<BotGetDetailsGameResponseViewItem>;
    winner: string;
  }
  
  export class UserGetDetailsGameResponseView
  {
      name:string;
      points: number;
      cards: Array<StepUserGetDetailsGameResponseViewItem>;

  }

  export class StepUserGetDetailsGameResponseViewItem
  {
      suit: number;
      rank: number;
  }

  export class BotGetDetailsGameResponseViewItem
  {
    name:string;
    points: number;
    cards: Array<StepBotGetDetailsGameResponseViewItem>;
  }

  export class StepBotGetDetailsGameResponseViewItem
  {
      suit: number;
      rank: number;
  }