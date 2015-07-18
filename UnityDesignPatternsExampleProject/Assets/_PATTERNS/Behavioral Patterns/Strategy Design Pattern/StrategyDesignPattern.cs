﻿using UnityEngine;
using System.Collections;

// USE when: 
// you need one of several behaviours dynamically
// when you have long lists of conditionals
// avoid duplicate code
// can hide complicated / secret code
// negative: increasing amount of classes


// short implementation of Derek Barnas' Strategy Design Pattern
namespace StrategyDesign
{
	public class StrategyDesignPattern : MonoBehaviour
	{
		void OnEnable ()
		{
			Debug.Log ("------------------");
			Debug.Log ("STRATEGY DESIGN PATTERN");
			// create dog and bird example objects
			Animal sparky = new Dog ();
			Animal tweety = new Bird ();

			Debug.Log ("Dog: " + sparky.TryToFly ());
			Debug.Log ("Bird: " + tweety.TryToFly ());

			// change behaviour of dog
			sparky.SetFlyingAbility (new ItFlys ());
		
			Debug.Log ("Dog: " + sparky.TryToFly ());
			Debug.Log ("Bird: " + tweety.TryToFly ());
		}
	}
	

	// Using Interfaces for decoupling
	// putting behaviour that varies in different classes
	public interface IFly
	{
		string Fly ();
	}

	// Class that holds behaviour for objects that can fly
	class ItFlys : IFly
	{
		public string Fly ()
		{
			return "Flying high";
		}
	}

	// Class that holds behaviour for objects that can not fly
	class CantFly : IFly
	{
		public string Fly ()
		{
			return "I can't fly";
		}
	}

	class FlyingSuperFast : IFly
	{
		public string Fly ()
		{
			return "Fly super fast";
		}
	}


	// Classes that hold an instance of the behaviours above:
	public class Animal
	{
		// hereby adding the behaviour
		// it also can change that way
		public IFly flyingType;

		public string TryToFly ()
		{
			return flyingType.Fly ();
		}

		public void SetFlyingAbility (IFly newFlyingType)
		{
			this.flyingType = newFlyingType;
		}
	}

	// derived objects that vary from the base Animal:
	public class Dog : Animal
	{
		public Dog ()
		{
			flyingType = new CantFly ();
		}
	}

	public class Bird : Animal
	{
		public Bird ()
		{
			flyingType = new ItFlys ();
		}
	}
}

// Rembember:
// ALWAYS
// eliminate duplicate code
// eliminate technices that allow one class to affect others