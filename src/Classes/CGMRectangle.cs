﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codessentials.CGM.Classes
{
    public struct CGMRectangle
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Height { get; private set; }
        public double Width { get; private set; }

        /// <summary>
        /// Represents an instance of the CGMRectangle class with its members uninitialized.
        /// </summary>
		public static readonly CGMRectangle Empty = default(CGMRectangle);

        public CGMRectangle(double x, double y, double width, double height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        /// <summary>Tests whether the <see cref="CGMRectangle.Width" /> or <see cref="CGMRectangle.Height" /> property of this <see cref="CGMRectangle" /> has a value of zero.</summary>
		/// <returns>This property returns true if the <see cref="CGMRectangle.Width" /> or <see cref="CGMRectangle.Height" /> property of this <see cref="CGMRectangle" /> has a value of zero; otherwise, false.</returns>
        public bool IsEmpty
        {
            get
            {
                return this.Width <= 0f || this.Height <= 0f;
            }
        }

        /// <summary>Determines if the specified point is contained within this rectangle.</summary>
        /// <returns>This method returns true if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained within this rectangle; otherwise false.</returns>
        /// <param name="point">The point to test. </param>
        public bool Contains(CGMPoint point)
        {
            return Contains(point.X, point.Y);
        }

        /// <summary>Determines if the specified point is contained within this rectangle.</summary>
        /// <returns>This method returns true if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained within this rectangle; otherwise false.</returns>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        public bool Contains(double x, double y)
        {
            return this.X <= x && x < this.X + this.Width && this.Y <= y && y < this.Y + this.Height;
        }

        /// <summary>
        /// Determines if the specified point is contained within this rectangle.
        /// </summary>
        /// <param name="point">The point to test.</param>
        /// <param name="maxDistance">The maximum distance to the rectangle border.</param>
        /// <returns>
        /// This method returns true if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained within this rectangle; otherwise false.
        /// </returns>
        public bool Contains(CGMPoint point, double maxDistance)
        {
            return Contains(point.X, point.Y, maxDistance);
        }

        /// <summary>Determines if the specified point is contained within this rectangle.</summary>
        /// <returns>This method returns true if the point defined by <paramref name="x" /> and <paramref name="y" /> is contained within this rectangle; otherwise false.</returns>
        /// <param name="x">The x-coordinate of the point to test. </param>
        /// <param name="y">The y-coordinate of the point to test. </param>
        /// <param name="maxDistance">The maximum distance to the rectangle border.</param>
        public bool Contains(double x, double y, double maxDinstance)
        {
            return (this.X <= x && this.X+maxDinstance >= x) && (x < this.X + this.Width && x+maxDinstance >= this.X + this.Width)
                && (this.Y <= y && this.Y+maxDinstance >= y) && (y < this.Y + this.Height && y+maxDinstance >= this.Y+this.Height);
        }

        /// <summary>
        /// Create a rectangle from the rectangle points.
        /// </summary>
        /// <param name="leftUpperCorner">The left upper corner.</param>
        /// <param name="rightUpperCorner">The right upper corner.</param>
        /// <param name="leftLowerCorner">The left lower corner.</param>
        /// <param name="rightLowerCorner">The right lower corner.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static CGMRectangle FromPoints(CGMPoint leftUpperCorner, CGMPoint rightUpperCorner, CGMPoint leftLowerCorner, CGMPoint rightLowerCorner)
        {            
            ValidationCorners(leftUpperCorner, rightUpperCorner, leftLowerCorner, rightLowerCorner);

            return new CGMRectangle(leftUpperCorner.X,  leftUpperCorner.Y, rightUpperCorner.X - leftUpperCorner.X,  leftLowerCorner.Y - leftUpperCorner.Y );
        }

        internal static void ValidationCorners(CGMPoint leftUpperCorner, CGMPoint rightUpperCorner, CGMPoint leftLowerCorner, CGMPoint rightLowerCorner)
        {
            if (!CGMPoint.IsSame(leftUpperCorner.Y, rightUpperCorner.Y))
                throw new ArgumentException("The left upper corner is not at the same height as the right upper corner");

            if (!CGMPoint.IsSame(leftLowerCorner.Y, rightLowerCorner.Y))
                throw new ArgumentException("The left lower corner is not at the same height as the right lower corner");

            if (!CGMPoint.IsSame(leftUpperCorner.X, leftLowerCorner.X))
                throw new ArgumentException("The left upper corner is not at the same X as the left lower corner");

            if (!CGMPoint.IsSame(rightUpperCorner.X, rightLowerCorner.X))
                throw new ArgumentException("The right upper corner is not at the same X as the right lower corner");
        }
    }
}