import { Component, OnInit } from '@angular/core';
import { GameService } from 'src/app/shared/services/game.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
  selector: 'app-start-game',
  templateUrl: './start-game.component.html',
  styleUrls: ['./start-game.component.scss']
})
export class StartGameComponent implements OnInit {

  constructor(private readonly formBuilder: FormBuilder, private readonly gameService: GameService, private readonly router: Router) {
    
  }
  
  private continue: boolean;
  private countBots: number = 0;


  public startGameForm = this.formBuilder.group({
    countBots: [1, [Validators.required, Validators.min(1)]],
  });



  public ngOnInit(): void {
    this.gameService.isUnfinished().subscribe(response => this.continue = response);
  }




  public startGame(): void {
    this.countBots = this.startGameForm.value.countBots;
    this.gameService.start(this.countBots).subscribe(response => {
      console.log(response);
      this.router.navigate(["/game/game-play"]);
    });

  }

}
