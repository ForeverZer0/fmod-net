using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using FMOD.Core;
using FMOD.Enumerations;
using FMOD.Structures;

namespace FMOD.Sharp.Controls
{
	public static class WaveformPathFactory
	{
		private static int _totalBytes;
		private static int _numPoints;
		private static int _chunkSize;
		private static BufferReader _bufferReader;
		private static PointF[] _maxPathPoints;
		private static PointF[] _avgPathPoints;
		private static float _normalizeX;
		private static float _normalizeY;

		public static void Create(FmodSystem system, string filename, out GraphicsPath maxPath, out GraphicsPath avgPath)
		{
			var sound = system.CreateStream(filename);
			Create(sound, out maxPath, out avgPath);
			sound.Dispose();
		}

		public static void Create(Sound sound, out GraphicsPath maxPath, out GraphicsPath avgPath)
		{
			_bufferReader = BufferReader.FromSound(sound);
			_numPoints = (int)(sound.GetLength() / (sound.DefaultFrequency / 1000));
			switch (sound.Format)
			{
				case SoundFormat.Pcm8:
					InitPath(1, byte.MaxValue);
					Read8Bit();
					break;
				case SoundFormat.Pcm16:
					InitPath(2, short.MaxValue);
					Read16Bit();
					break;
				case SoundFormat.Pcm24:
					InitPath(3, Int24.MaxValue);
					Read24Bit();
					break;
				case SoundFormat.Pcm32:
					InitPath(4, int.MaxValue);
					Read32Bit();
					break;
				case SoundFormat.PcmFloat:
					InitPath(4, 1.0f);
					Read32BitFloat();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			maxPath = BuildPath(_maxPathPoints);
			avgPath = BuildPath(_avgPathPoints);
			Free();
		}

		private static void Free()
		{
			_bufferReader = null;
			_maxPathPoints = null;
			_avgPathPoints = null;
			GC.Collect();
		}

		private static void InitPath(int unitByteSize, float maxValue)
		{
			_totalBytes = _bufferReader.ByteBuffer.Length;
			_chunkSize = _totalBytes / _numPoints / unitByteSize;
			_normalizeX = 1.0f / _numPoints;
			_normalizeY = 1.0f / maxValue;
			_maxPathPoints = new PointF[_numPoints];
			_avgPathPoints = new PointF[_numPoints];
		}

		private static GraphicsPath BuildPath(PointF[] points)
		{
			var path = new GraphicsPath();
			path.AddLines(points);
			using (var clone = (GraphicsPath) path.Clone())
			{
				using (var matrix = new Matrix(1, 0, 0, -1, 0, 0))
				{
					clone.Transform(matrix);
				}
				clone.Reverse();
				path.AddLines(clone.PathPoints);
			}
			return path;
		}

		private static void Read8Bit()
		{
			unchecked
			{
				Parallel.For(0, _numPoints, pointIndex =>
				{
					var max = 0f;
					var avg = 0f;
					var accepted = _bufferReader.ByteBuffer.Length;
					var offset = pointIndex * _chunkSize;
					var chunkEnd = offset + Math.Min(_chunkSize, _totalBytes - offset);
					for (var i = offset; i < chunkEnd; i++)
					{
						var value = _bufferReader.ByteBuffer[i];
						if (value > max)
							max = value;
						avg += value;
					}
					_maxPathPoints[pointIndex] = new PointF(pointIndex * _normalizeX, max * _normalizeY);
					_avgPathPoints[pointIndex] = new PointF(pointIndex * _normalizeX, (avg / accepted) * _normalizeY);
				});
			}
		}

		private static void Read16Bit()
		{
			unchecked
			{
				Parallel.For(0, _numPoints, pointIndex =>
				{
					var max = 0f;
					var avg = 0f;
					var accepted = 0;
					var offset = pointIndex * _chunkSize;
					var chunkEnd = offset + Math.Min(_chunkSize, _totalBytes - offset);
					for (var i = offset; i < chunkEnd; i++)
					{
						var value = _bufferReader.Int16Buffer[i];
						if (value < 0)
							continue;
						if (value > max)
							max = value;
						avg += value;
						accepted++;
					}
					_maxPathPoints[pointIndex] = new PointF(pointIndex * _normalizeX, max * _normalizeY);
					_avgPathPoints[pointIndex] = new PointF(pointIndex * _normalizeX, (avg / accepted) * _normalizeY);
				});
			}
		}

		private static void Read24Bit()
		{
			unchecked
			{
				Parallel.For(0, _numPoints, pointIndex =>
				{
					var max = 0f;
					var avg = 0f;
					var accepted = 0;
					var offset = pointIndex * _chunkSize;
					var chunkEnd = offset + Math.Min(_chunkSize, _totalBytes - offset);
					for (var i = offset; i < chunkEnd; i++)
					{
						var value = _bufferReader.Int24Buffer[i].Value;
						if (value < 0)
							continue;
						if (value > max)
							max = value;
						avg += value;
						accepted++;
					}
					_maxPathPoints[pointIndex] = new PointF(pointIndex * _normalizeX, max * _normalizeY);
					_avgPathPoints[pointIndex] = new PointF(pointIndex * _normalizeX, (avg / accepted) * _normalizeY);
				});
			}
		}

		private static void Read32Bit()
		{
			unchecked
			{
				Parallel.For(0, _numPoints, pointIndex =>
				{
					var max = 0f;
					var avg = 0f;
					var accepted = 0;
					var offset = pointIndex * _chunkSize;
					var chunkEnd = offset + Math.Min(_chunkSize, _totalBytes - offset);
					for (var i = offset; i < chunkEnd; i++)
					{
						var value = _bufferReader.Int32Buffer[i];
						if (value < 0)
							continue;
						if (value > max)
							max = value;
						avg += value;
						accepted++;
					}
					_maxPathPoints[pointIndex] = new PointF(pointIndex * _normalizeX, max * _normalizeY);
					_avgPathPoints[pointIndex] = new PointF(pointIndex * _normalizeX, (avg / accepted) * _normalizeY);
				});
			}
		}

		private static void Read32BitFloat()
		{
			unchecked
			{
				Parallel.For(0, _numPoints, pointIndex =>
				{
					var max = 0f;
					var avg = 0f;
					var accepted = 0;
					var offset = pointIndex * _chunkSize;
					var chunkEnd = offset + Math.Min(_chunkSize, _totalBytes - offset);
					for (var i = offset; i < chunkEnd; i++)
					{
						var value = _bufferReader.FloatBuffer[i];
						if (value < 0)
							continue;
						if (value > max)
							max = value;
						avg += value;
						accepted++;
					}
					_maxPathPoints[pointIndex] = new PointF(pointIndex * _normalizeX, max);
					_avgPathPoints[pointIndex] = new PointF(pointIndex * _normalizeX, avg / accepted);
				});
			}
		}
	}
}
