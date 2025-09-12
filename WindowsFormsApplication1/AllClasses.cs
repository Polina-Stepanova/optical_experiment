using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;


namespace OpticalExperiment
{
    public enum LensType { Плосковыпуклая=1, Двояковыпуклая, СобМениск, Плосковогнутая, Двояковогнутая, РассМениск }


    [Serializable]
    public class Experiment 
    {
        List<Lens> Lenses;//only first, real, obj
        List<ObjectImage> ObjImages;//images only, obj 2-last
        ObjectImage SourceObj;
        public bool scaleline_visibility,ray_visibility;
        public sbyte selected_lens_id,selected_obj_image_id,lens_id_editing;
        public Experiment()
        {
            Lenses = new List<Lens>();
            ObjImages = new List<ObjectImage>();
            SourceObj = new ObjectImage(); //a default,no point in it not existing.  x=0, real, size=1, up
            ObjImages.Add(SourceObj);
            scaleline_visibility = true;
            ray_visibility = true;
            selected_lens_id = -1; selected_obj_image_id = -1; lens_id_editing = -1;
        }

        public sbyte Which_Lens_hit(float x)
        {
            selected_lens_id = -1;
            for (sbyte i = 0; i < Lenses.Count; i++)
            { if (Lenses[i].Is_clicked(x)) { selected_lens_id = i; break; } }
            return selected_lens_id;
        }
        public sbyte Which_Image_hit(float x)
        {
            selected_obj_image_id = -1;
            if (ObjImages.Count > 1) {
                for (sbyte i = 0; i < ObjImages.Count; i++)
                { if (ObjImages[i].Is_clicked(x)) { selected_obj_image_id = i; break; } }
            }
            return selected_obj_image_id;
        }
        public int N_Lenses()
        { return Lenses.Count(); }
        public int N_ObjImages()
        { return ObjImages.Count(); }
        public ObjectImage ExperRez()
        {
            if (ObjImages.Count() >= 2) return ObjImages.Last();
            return null;
        }
        public ObjectImage OrigObj()
        {
            ObjectImage OrigCopy = new ObjectImage(SourceObj);
            return OrigCopy;
        }
        public ObjectImage ViewImage(sbyte i)
        {
            ObjectImage ObjImCopy;
            if (i >= 0 & i < ObjImages.Count())
            { ObjImCopy = new ObjectImage(ObjImages[i]); return ObjImCopy; }
            return null;
        }
        public void EditOrigObj(ObjectImage NewOrigObj)
        {
            ObjImages.RemoveAt(0);
            SourceObj = new ObjectImage(NewOrigObj);
            ObjImages.Insert(0, SourceObj);
        }

        public void ConstructResImage()
        {
            ObjImages = new List<ObjectImage>();
            ObjImages.Add(SourceObj);

            if (Lenses.Count() > 0) {
                ObjectImage temp, temprez;
                temp = SourceObj;
                for (int i = 0; i < Lenses.Count(); i++)
                {
                    temprez = Lenses[i].Construct_image(temp);
                    ObjImages.Add(temprez);
                    if (temprez.Infin()) break;
                    temp = temprez;
                }
            }

        }
        public void Draw(Graphics g, int x, int y)
        {
            Pen p1 = new Pen(Color.Black,1);
            g.DrawLine(p1,0,y,x,y);
            SourceObj.Draw(g,y);
            for (int i=0;i<Lenses.Count;i++)
            { if(i== selected_lens_id) Lenses[i].Draw(g, y, Color.Blue); else Lenses[i].Draw(g, y, Color.Black); }
            (ObjImages.Last()).Draw(g,y);
            if (scaleline_visibility) Draw_Scale(g,x,y);
            if (ray_visibility) Draw_Rays(g,y);

        }
        public void Draw_Rays(Graphics g, int y)//if "show rays" is selected - all steps of images+rays
        {
            if (ObjImages.Count() >= 2)
            {
                float size_dir,size_dir_1;
                Pen p1 = new Pen(Color.Gray, 1);
                foreach (ObjectImage ob in ObjImages) { if((!ob.Infin())&(!(ob.ShowX()==-1))) ob.Draw(g, y); }

                size_dir = (ObjImages[0].OrUp()) ? ObjImages[0].ShowSize() : ObjImages[0].ShowSize() * -1;
                if (ObjImages[0].Infin()) { g.DrawLine(p1, 0, y- size_dir, Lenses[0].ShowX(), y- size_dir); }
                else {
                    g.DrawLine(p1, ObjImages[0].ShowX(), y- size_dir, Lenses[0].ShowX(), y- size_dir);
                    g.DrawLine(p1, ObjImages[0].ShowX(), y- size_dir, Lenses[0].ShowX(), y);
                }

                for (int i = 1; i < ObjImages.Count(); i++)
                {
                    size_dir = (ObjImages[i - 1].OrUp()) ? ObjImages[i - 1].ShowSize() : ObjImages[i - 1].ShowSize() * -1;

                    if (ObjImages[i].Infin())
                    {
                        if (Math.Abs(ObjImages[i - 1].ShowX() - Lenses[i - 1].ShowX()) == Lenses[i - 1].ShowFdist())//the parallel lines from an obj standing in lense's focal point
                        { 
                            g.DrawLine(p1, Lenses[i - 1].ShowX(), y, Lenses[i - 1].ShowX() + (Lenses[i - 1].ShowX() - ObjImages[i].ShowX()), y + size_dir);//the ray through the lenses center doesnt refract
                            g.DrawLine(p1, Lenses[i - 1].ShowX(), y - size_dir, Lenses[i - 1].ShowX() + (Lenses[i - 1].ShowX() - ObjImages[i].ShowX()), y);
                        }
                        //other options include: parallel lines hitting conv/disp lens and conv/imagin.conv in focal points
                        else if(ObjImages[i - 1].Infin())
                        {
                            if (Lenses[i - 1].ShowLtype() >= (LensType)1 & Lenses[i - 1].ShowLtype() <= (LensType)3) {
                                if (Lenses[i - 1].ShowX()> ObjImages[i - 1].ShowX()) g.DrawLine(p1, Lenses[i - 1].ShowX(), y- size_dir, Lenses[i - 1].ShowX()+ Lenses[i - 1].ShowFdist(), y );
                                else g.DrawLine(p1, Lenses[i - 1].ShowX(), y- size_dir, Lenses[i - 1].ShowX() - Lenses[i - 1].ShowFdist(), y);
                            }//conv ===|>
                            else {
                                if (Lenses[i - 1].ShowX() > ObjImages[i - 1].ShowX()) g.DrawLine(p1, Lenses[i - 1].ShowX(), y- size_dir, Lenses[i - 1].ShowX() - Lenses[i - 1].ShowFdist(), y);
                                else g.DrawLine(p1, Lenses[i - 1].ShowX(), y- size_dir, Lenses[i - 1].ShowX() + Lenses[i - 1].ShowFdist(), y);

                            }//diverg ===<=|<
                        }

                        break;
                    }
                    else
                    {
                        size_dir_1= (ObjImages[i ].OrUp()) ? ObjImages[i].ShowSize() : ObjImages[i ].ShowSize() * -1;
                        g.DrawLine(p1, ObjImages[i].ShowX(), y- size_dir_1, Lenses[i-1].ShowX(), y);
                        g.DrawLine(p1, ObjImages[i].ShowX(), y- size_dir_1, Lenses[i-1].ShowX(), y- size_dir);
                        if (i < ObjImages.Count() - 1)//last one has no next lens to send rays to
                        {
                            g.DrawLine(p1, ObjImages[i].ShowX(), y - size_dir_1, Lenses[i].ShowX(), y - size_dir_1);
                            g.DrawLine(p1, ObjImages[i].ShowX(), y - size_dir_1, Lenses[i].ShowX(), y);
                        }
                    }
                }

            }

        }
        public void Draw_Scale(Graphics g, int x,int y)
        {
            Pen p1 = new Pen(Color.Black, 1);
            for (int i = 0;  i <= x; i += 10) { g.DrawLine(p1,i,y*2,i, y*2 - 7); }

        }
        public void Add_Lens(Lens L)
        {
            Lenses.Add(L);
            Rearrange_lenses();
        }
        public Lens Remove_Lens(sbyte lens_id)
        {
            Lens l_temp;
            if (Lenses[lens_id].ShowLtype() >= (LensType)1 & Lenses[lens_id].ShowLtype() <= (LensType)3)
                l_temp = new ConvergingLens(Lenses[lens_id].ShowX(), Lenses[lens_id].ShowFdist(), Lenses[lens_id].ShowLtype());
            else l_temp = new DivergingLens(Lenses[lens_id].ShowX(), Lenses[lens_id].ShowFdist(), Lenses[lens_id].ShowLtype());
            Lenses.RemoveAt(lens_id);
            return l_temp;
        }
        public void Rearrange_lenses()
        {
            selected_lens_id = -1;

            Lenses.Sort(CompareLensPositions);
            if (SourceObj.ShowX() >= Lenses.Last().ShowX()) { Lenses.Reverse(); }

            ConstructResImage(); 
        }
        public static int CompareLensPositions(Lens L1, Lens L2)
        {
            return L1.ShowX().CompareTo(L2.ShowX());
        }
    }


    [Serializable]
    abstract public class Lens
    {
        protected float x;
        protected float foc_dist;
        protected LensType type;//name of lens type as enumerated type
        protected ObjectImage orig_obj,created_image;

        public Lens(float x=20.0F, float foc_dist=10.0F, LensType type=(LensType)1)
        {
            this.x = x;this.foc_dist = foc_dist;this.type = type;
        }
        public Lens(Lens og)
        {
            x = og.x; foc_dist = og.foc_dist;type = og.type; orig_obj = og.orig_obj; created_image = og.created_image;
        }
        public abstract void Draw(Graphics g, int y, Color c);
        public float ShowX() { return x; }
        public float ShowFdist() { return foc_dist; }
        public LensType ShowLtype() { return type; }
        public bool Is_clicked(float mouse_x_pos)
        {
            if ((mouse_x_pos - x) <= 2 & (mouse_x_pos - x) >= -2) return true; 
            return false;
        }
        public ObjectImage Construct_image(ObjectImage orig_obj)
        {
            this.orig_obj = orig_obj;       
            if (x == orig_obj.ShowX())
            { created_image = new ObjectImage(orig_obj.ShowX(), false, orig_obj.ShowSize(), orig_obj.OrUp(), false, orig_obj.ReOrIm());
                return created_image; }

            if (orig_obj.Infin()) {
                if((type>=(LensType)1) & (type <= (LensType)3)) {
                    created_image = new ObjectImage(x+foc_dist,true,0, orig_obj.OrUp(),false,true); }//conv
                else { created_image = new ObjectImage(x - foc_dist,true,0, orig_obj.OrUp(), false, false); }

            }
            else {
                if (((type >= (LensType)1) & (type <= (LensType)3)) &(orig_obj.ShowX() == x + foc_dist || orig_obj.ShowX() == x - foc_dist))//in foc point of conv lens
                {
                    created_image = new ObjectImage(-1, true, -1, orig_obj.OrUp(), false, false);
                }
                else {

                    float x_im, size_im;
                    bool  ortup, imtype;
                    if (Math.Abs(orig_obj.ShowX() - x) < foc_dist || ((type >= (LensType)4) & (type <= (LensType)6))) {
                        imtype = false;  }
                    else {
                        imtype = true;  }

                    if ((type >= (LensType)4) & (type <= (LensType)6)) {
                        PointF p1,p2,f1,f2;
                        float size_dir = (orig_obj.OrUp()) ? orig_obj.ShowSize() : orig_obj.ShowSize() * -1;
                        p1 = new PointF(orig_obj.ShowX(), size_dir);
                        p2 = new PointF(x, 0);
                        f1 = new PointF(x, size_dir);
                        if (x> orig_obj.ShowX())  f2 = new PointF(x-foc_dist, 0);
                        else  f2 = new PointF(x+foc_dist, 0);

                        //(p2-p1)*k1 + p1(верхняя точка исх) == (f2-f1)*k2+f1(исх точка перпендик луча по преломлении в линзе)
                        //k1==(  (f2.x-f1.x)*k2+f1.x-p1.x ) /(p2.x-p1.x)
                        float k2 = ((f1.Y - p1.Y) * (p2.X - p1.X) - (p2.Y - p1.Y) * (f1.X - p1.X)) / ((p2.Y - p1.Y) * (f2.X - f1.X) - (f2.Y - f1.Y) * (p2.X - p1.X));
                        x_im = (f2.X - f1.X) * k2 + f1.X;
                        size_im = (f2.Y - f1.Y) * k2 + f1.Y;
                        ortup = (size_im >= 0) ? true : false;
                        /*if (!ortup)*/ size_im = Math.Abs(size_im);


                    }
                    else 
                    {
                        PointF p1, p2, f1, f2;
                        float size_dir = (orig_obj.OrUp()) ? orig_obj.ShowSize() : orig_obj.ShowSize() * -1;

                        p1 = new PointF(orig_obj.ShowX(), size_dir);
                        p2 = new PointF(x, 0);
                        f1 = new PointF(x, size_dir);
                        if (orig_obj.ShowX() < x)
                            f2 = new PointF(x + foc_dist, 0);
                        else f2 = new PointF(x - foc_dist, 0);
                        float k2 = ((f1.Y - p1.Y) * (p2.X - p1.X) - (p2.Y - p1.Y) * (f1.X - p1.X)) / ((p2.Y - p1.Y) * (f2.X - f1.X) - (f2.Y - f1.Y) * (p2.X - p1.X));
                        x_im = (f2.X - f1.X) * k2 + f1.X;
                        size_im = (f2.Y - f1.Y) * k2 + f1.Y;
                        ortup = (size_im >= 0) ? true : false;
                        /*if (!ortup)*/ size_im = Math.Abs(size_im);
                    }
                    created_image = new ObjectImage(x_im, false, size_im, ortup, false, imtype);

                }

            }
            return created_image;
        }

    }

    [Serializable]
    public class DivergingLens : Lens
    {
        public DivergingLens(float x, float foc_dist, LensType type) :base(x,foc_dist,type) { }
        public override void Draw(Graphics g, int y, Color c) 
        {
            Pen p1 = new Pen(c, 1);
            g.DrawLine(p1,x,y-100,x,y+100);
            g.DrawLine(p1, x, y - 100, x+5, y - 105); g.DrawLine(p1, x, y - 100, x-5, y - 105);//arrow up point up
            g.DrawLine(p1, x, y + 100, x+5, y + 105); g.DrawLine(p1, x, y + 100, x-5, y + 105);//arrow down point down
        }

    }

    [Serializable]
    public class ConvergingLens : Lens
    {
        public ConvergingLens(float x, float foc_dist, LensType type) :base(x,foc_dist,type) { }
        public override void Draw(Graphics g, int y, Color c)
        {
            Pen p1 = new Pen(c, 1);
            g.DrawLine(p1, x, y - 100, x, y + 100);
            g.DrawLine(p1, x, y - 100, x+5, y - 95); g.DrawLine(p1, x, y - 100, x-5, y - 95); //arrow up point down
            g.DrawLine(p1, x, y + 100, x+5, y + 95); g.DrawLine(p1, x, y + 100, x-5, y + 95);//arrow down point up 
        }
    }

    [Serializable]
    public class ObjectImage
    {
        float x; bool inf_distance;
        float size;bool orient_up;
        bool object_or_image;
        bool real_or_imgin;//which image type if first flag is false


        public ObjectImage()
        {
            x = 0; inf_distance = false;
            size = 50;orient_up = true;
            object_or_image = true;  //==real, an object
            real_or_imgin = true;
        }
        public ObjectImage(float x, bool infdist, float size, bool ort_up, bool is_orig_obj, bool image_type)
        {
            inf_distance = infdist;
            if (infdist) this.x = -1; else this.x=x; 
            this.size = size;
            orient_up = ort_up;
            object_or_image = is_orig_obj;
            real_or_imgin = image_type;
        }
        public ObjectImage(ObjectImage og)
        {
            x = og.x;
            inf_distance = og.inf_distance;
            size = og.size;
            orient_up = og.orient_up;
            object_or_image = og.object_or_image;
            real_or_imgin = og.real_or_imgin;
        }
        public float ShowX() { return x; }
        public float ShowSize() { return size; }
        public bool Infin() { return inf_distance; }
        public bool OrUp() { return orient_up; }
        public bool ReOrIm() { return real_or_imgin; }
        public bool ObOrIm() { return object_or_image; }


        public void Draw(Graphics g, int y)
        {
            if (!(inf_distance)){
                Pen p1;
                if (object_or_image) p1 = new Pen(Color.Red, 3);
                else
                {
                    if (real_or_imgin) p1 = new Pen(Color.Orange, 3);//image but real
                    else p1 = new Pen(Color.Green, 3); //image and imaginary
                }

                    if (orient_up)
                    {
                        g.DrawLine(p1, x, y, x, y - size);
                        g.DrawLine(p1, x, y - size, x + 5, y - size + 5); g.DrawLine(p1, x, y - size, x - 5, y - size + 5);//pointy arrow parts
                    }
                    else
                    {
                        g.DrawLine(p1, x, y, x, y + size);
                        g.DrawLine(p1, x, y + size, x + 5, y + size - 5); g.DrawLine(p1, x, y + size, x - 5, y + size - 5);
                    }


            }
        }
        public bool Is_clicked(float mouse_x_pos)
        {
            if ((mouse_x_pos - x) <= 2 & (mouse_x_pos - x) >= -2) return true; 
            return false;
        }
    }
    //save and load functions moved from originally envisioned in-class to  Form1 methods
}
