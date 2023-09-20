import {Component, OnInit} from '@angular/core';
import {JacobiService} from "../../services/jacobi.service";
import {JacobiRequest} from "../../dtos/JacobiRequest";

@Component({
  selector: 'app-jacobi-method',
  templateUrl: './jacobi-method.component.html',
  styleUrls: ['./jacobi-method.component.css']
})
export class JacobiMethodComponent implements OnInit {

  constructor(public jacobiService: JacobiService) {
    this.generateRandom()
  }

  a: number[][] = [
    [0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0],
    [0, 0, 0, 0, 0]
  ]
  b: number[] = [0, 0, 0, 0, 0]
  x: number[] = [0, 0, 0, 0, 0]

  error: number = 0.001;

  ngOnInit(): void {
  }

  calculate() {
    const request: JacobiRequest = {
      error: this.error,
      a: this.a,
      b: this.b
    }

    this.jacobiService.calculateJacobi(request).subscribe(response => {
      this.x = response.x
    })
  }

  private generateRandom() {
    for (let i = 0; i < this.a.length; i++) {
      for (let j = 0; j < this.a[i].length; j++) {
        this.a[i][j] = this.randomIntFromInterval(-5, 5)
      }
    }

    for (let i = 0; i < this.a.length; i++) {
      this.b[i] = this.randomIntFromInterval(-10, 10)
    }
  }

  private randomIntFromInterval(min: number, max: number) {
    return Math.floor(Math.random() * (max - min + 1) + min)
  }
}
